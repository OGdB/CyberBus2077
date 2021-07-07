using UnityEngine;
using System.Collections;

public class BoltSystem : MonoBehaviour
{
    public BoltSystemObj attachedObj;
    public Bolt bolt;

    public AudioSource wrenchSound;

    public Transform anchorTransform;
    public GameObject objectPlaceholder;
    public GameObject objectAnchor;
    public float anchoringDistance;

    public void UseBolt()
    {
        attachedObj.isLockedInPlace = bolt.isClosed;
        if (!wrenchSound.isPlaying)
        {
            wrenchSound.Play();
        }
    }

    public void DetachObj()
    {
        attachedObj = null;
        bolt.canBolt = false;
        objectPlaceholder.SetActive(true);
    }

    public void AttachObj(BoltSystemObj obj)
    {
        attachedObj = obj;
        bolt.canBolt = true;
        objectAnchor.SetActive(false);
        objectPlaceholder.SetActive(false);
    }

    public void ObjectNoLongerHovering()
    {
        if (attachedObj == null)
        {
            objectAnchor.SetActive(false);
            objectPlaceholder.SetActive(true);
        }
    }

    public void ObjectHovering ()
    {
        if (attachedObj == null)
        {
            objectAnchor.SetActive(true);
            objectPlaceholder.SetActive(false);
        }
    }

    public bool IsSystemBuilt()
    {
        return attachedObj != null && bolt.isClosed;
    }

    public bool IsSystemFunctional ()
    {
        return this.IsSystemBuilt() && attachedObj.isObjectFunctional;
    }
}
