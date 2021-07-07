using UnityEngine;
using System.Collections;

public class Bolt : Highlightable
{
    public bool isClosed;
    public bool canBolt;
    public GameObject boltClosedGO;
    public GameObject boltOpenGO;

    public override void Interact(Tool t)
    {
        if (canBolt)
        {
            isClosed = !isClosed;
            boltClosedGO.SetActive(!boltClosedGO.activeSelf);
            boltOpenGO.SetActive(!boltOpenGO.activeSelf);

            SendMessageUpwards("UseBolt");
        }
    }
}
