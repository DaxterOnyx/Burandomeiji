using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFacingVR : MonoBehaviour {

    Camera cam;

    void Update()
    {
        if(cam == null)
        {
            cam = Camera.main;
        }
        else
        {
            transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
        }
    }
}
