using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrallaxBackground : MonoBehaviour
{

    private Transform theCamera;
    public Transform sky, treeline;
    public float skyOffset=0;
    [Range(0f, 1f)]
    public float parallaxSpeed;

    void Start()
    {
        theCamera = Camera.main.transform;
    }

    void LateUpdate()
    {
        sky.position = new Vector3(theCamera.position.x, sky.position.y, sky.position.z);
        treeline.position = new Vector3(theCamera.position.x * parallaxSpeed, treeline.position.y, treeline.position.z);
    }
}
