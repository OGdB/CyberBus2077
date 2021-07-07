using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkybox : MonoBehaviour
{
    public float speed = 1f;
    public float skyboxExposure = 1.04f;

    private void Start()
    {
        skyboxExposure = 1.04f;
    }
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * speed);
        RenderSettings.skybox.SetFloat("_Exposure", skyboxExposure);
    }
}
