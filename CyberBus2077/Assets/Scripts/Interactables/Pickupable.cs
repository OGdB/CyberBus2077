using UnityEngine;
using System.Collections;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(VelocityEstimator))]
public class Pickupable : Highlightable
{
    [Header("Pickupable")]
    public Rigidbody rb;
    public Transform originalParent;
    public bool isLockedInPlace;
    public bool isBeingHeld;
    public Color lockedHighlightColor;

    public int pickedUpLayer;
    public int interactableLayer;

    private VelocityEstimator estimator;

    private void Awake()
    {
        estimator = GetComponent<VelocityEstimator>();
    }

    public override void Interact(Tool t)
    {
        Debug.Log("Interacting as a pickupable");
    }

    public override void Highlight()
    {
        outline.OutlineColor = isLockedInPlace ? lockedHighlightColor : highlightColor;
        base.Highlight();
    }

    public override void UnHighlight()
    {
        base.UnHighlight();
    }

    public virtual void OnAttachedToHand(Hand hand)
    {
        UnHighlight();
        rb.isKinematic = true;
        gameObject.layer = pickedUpLayer;
        isBeingHeld = true;

        //Throwable
        estimator.BeginEstimatingVelocity();
    }

    public virtual void OnDetachedFromHand(Hand hand)
    {
        transform.parent = originalParent;
        rb.isKinematic = false;
        gameObject.layer = interactableLayer;
        isBeingHeld = false;

        // Throwable
        rb.AddForce(estimator.GetAccelerationEstimate() * 5f, ForceMode.Acceleration);
        rb.angularVelocity = estimator.GetAngularVelocityEstimate();
        estimator.FinishEstimatingVelocity();
    }
}
