using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class ToolInteractionRaycast : MonoBehaviour
{
    public float rayStartWidth;
    public float rayEndWidth;
    public float rayDistance;
    private Vector3 rayEndpoint;
    public Color rayIdleColor;
    public Color rayInteractColor;
    public LineRenderer rayLineRenderer;

    public LayerMask raycastMask;
    [SerializeField] private Highlightable lastHL;

    void Start()
    {
        rayLineRenderer.startWidth = rayStartWidth;
        rayLineRenderer.endWidth = rayEndWidth;
    }

    public Highlightable PerformRaycast(bool showRay)
    {
        rayLineRenderer.enabled = showRay;
        Highlightable highlightable = null;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance, raycastMask))
        {
            highlightable = hit.collider.GetComponentInChildren<Highlightable>();

            if (lastHL == null)
            {
                lastHL = highlightable;
                lastHL.Highlight();
            } // If you directly switched from interactable- to interactable without pointing at 'nothing'
            else if (lastHL.GetInstanceID() != highlightable.GetInstanceID())
            {
                lastHL.UnHighlight();
                lastHL = highlightable;
                lastHL.Highlight();
            }

            // Update Line renderer according to new highlightable
        }

        else if (lastHL != null)
        {
            lastHL.UnHighlight();
            lastHL = null;
        }

        UpdateLineRenderer(highlightable);
        return highlightable;

    }

    private void UpdateLineRenderer(Highlightable hl)
    {
        // Check what the player is pointing at > > Change line color and endPos accordingly
        if (hl != null)
        {
            rayEndpoint = hl.transform.position; // Snap pointer-end to the interactable object
            rayLineRenderer.material.color = rayInteractColor; // Line Color confirms you're pointing at an interactable
        }
        else
        {
            rayEndpoint = transform.position + base.transform.forward * rayDistance;
            rayLineRenderer.material.color = rayIdleColor; // Idle line color
        }

        rayLineRenderer.SetPosition(0, transform.position); // Line StartPos
        rayLineRenderer.SetPosition(1, rayEndpoint); // Line StartPos
    }
}
