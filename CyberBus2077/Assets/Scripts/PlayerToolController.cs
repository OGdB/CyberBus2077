using UnityEngine;
using System.Collections;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerToolController : MonoBehaviour
{
    //public Tool heldTool; // The tool that you're currently holding 
    public Pickupable heldObj;
    public EmptyHands emptyHandsTool;
    public bool isHoldingObj;
    public Highlightable hoveredObj; // Interactable object you're currently looking at

    public SteamVR_Action_Boolean interactBtn;
    public SteamVR_Action_Boolean pickUpBtn;
    private bool pickupClickWasDrop;

    public Hand hand;
    public Transform toolAttachPoint;
    public Transform pickupableAttachPoint;

    void Update()
    {
        bool isHeldObjATool = heldObj != null && heldObj is Tool;

        I_InteractableFinder currIF = emptyHandsTool;
        if (isHoldingObj)
        {
            currIF = null;
            if (isHeldObjATool)
            {
                currIF = (Tool)heldObj;
            }
        }
        FindInteractable(currIF);

        if (!isHoldingObj || isHeldObjATool) // If I'm holding something that is a tool, or I am not holding anything
        {
            // Interact btn pressed & we're looking at an interactable
            if (interactBtn.stateUp && hoveredObj != null)
            {
                InteractWithHoveredObj(hoveredObj, (Tool)heldObj);
            }
        }

        // Pick up / Put down btn presed
        if (!isHoldingObj && pickUpBtn.stateUp && hoveredObj != null && hoveredObj is Pickupable && !pickupClickWasDrop)
        {
            PickUpObj((Pickupable)hoveredObj);
        } else if (isHoldingObj && pickUpBtn.stateDown)
        {
            DropDownObj();
            pickupClickWasDrop = true;
        }

        if (pickUpBtn.stateUp)
        {
            pickupClickWasDrop = false;
        }
    }

    void FindInteractable(I_InteractableFinder iFinder)
    {
        if (iFinder != null)
            hoveredObj = iFinder.FindInteractable((!isHoldingObj && pickUpBtn.state && !pickupClickWasDrop) || (isHoldingObj && interactBtn.state));
        else
            hoveredObj = null;
    }

    public void PickUpObj(Pickupable pu)
    {
        if (pu.isLockedInPlace)
        {
            // Show some indication that it is locked
        }
        else
        {
            emptyHandsTool.DeactivateHands(); // Deactivate empty hands tool

            // Pick up, attach, and activate new tool
            heldObj = pu;
            heldObj.transform.position = toolAttachPoint.position;
            heldObj.transform.rotation = toolAttachPoint.rotation;
            
            if (pu is Tool)
            {
                hand.AttachObject(heldObj.gameObject, GrabTypes.Grip, Hand.AttachmentFlags.ParentToHand, toolAttachPoint);
            } else
            {
                hand.AttachObject(heldObj.gameObject, GrabTypes.Grip, Hand.AttachmentFlags.ParentToHand, pickupableAttachPoint);
            }

            isHoldingObj = true;
        }
    }

    void DropDownObj()
    {
        // Detach, put down and deactivate old tool
        hand.DetachObject(heldObj.gameObject, true);

        heldObj = null;
        isHoldingObj = false;

        // Activate hands again
        emptyHandsTool.ActivateHands();
    }

    void InteractWithHoveredObj(Highlightable hoveredObj, Tool toolUsed)
    {
        hoveredObj.Interact(toolUsed);
    }
}
