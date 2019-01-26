using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFacingVR : MonoBehaviour {

    Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if(cam != null)
            transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
    }
}
