using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;


public class Interacting : MonoBehaviour
{
    private bool isHoldingObject = false;

    public GameObject holdingObject;

    // Interact with object pointed at by raycast (except if holding object)
    public void Interact(Hand hand, GameObject interactable)
    {
        if (!isHoldingObject) // if not holding object > attach
        {
            holdingObject = interactable.gameObject;
            interactable.transform.position = hand.transform.position;
            hand.AttachObject(interactable, GrabTypes.Grip, Hand.AttachmentFlags.ParentToHand);
            isHoldingObject = true;
        }
        else // if holding object > detach
        {
            hand.DetachObject(holdingObject, true);
            print("drop it!");
            isHoldingObject = false;
            holdingObject = null;
        }
    }

}
