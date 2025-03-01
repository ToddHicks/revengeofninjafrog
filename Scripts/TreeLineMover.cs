using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeLineMover : MonoBehaviour
{
    public float maxDistance = 22f; // how far off the camera to be moved.
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromCamera = transform.position.x - Camera.main.transform.position.x;
        if(distanceFromCamera > maxDistance){
            // move to the left.
            transform.position -= new Vector3(maxDistance * 2f, 0f, 0f);
        }
        else if(distanceFromCamera < -maxDistance){
            // move to the right.
            transform.position += new Vector3(maxDistance * 2f, 0f, 0f);
        }
    }
}
