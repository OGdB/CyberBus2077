using UnityEngine;
using System.Collections;

public class EmptyHands : MonoBehaviour, I_InteractableFinder
{
    public ToolInteractionRaycast tRaycast;

    public Highlightable FindInteractable(bool showRay)
    {
        return tRaycast.PerformRaycast(showRay);
    }

    public void ActivateHands()
    {
        gameObject.SetActive(true);
    }

    public void DeactivateHands()
    {
        gameObject.SetActive(false);
    }
}
