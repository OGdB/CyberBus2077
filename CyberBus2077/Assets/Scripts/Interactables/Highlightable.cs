using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlightable : MonoBehaviour
{
    [Header("Highlightable")]
    public Outline outline;
    public Color highlightColor;

    void Start()
    {
        if (outline != null)
        {
            outline.OutlineColor = highlightColor;
        }
    }

    public virtual void Highlight()
    {
        outline.enabled = true;
    }
    public virtual void UnHighlight()
    {
        outline.enabled = false;
    }

    public virtual void Interact(Tool t)
    {
        Debug.Log("Interacted with a highlightable with a tool");
    }
}
