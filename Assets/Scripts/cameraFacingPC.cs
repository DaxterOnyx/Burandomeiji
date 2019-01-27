using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* */
public class cameraFacingPC : MonoBehaviour {

    GameObject camGO;
    Camera cam;

    private void Start()
    {
        camGO = GameObject.FindGameObjectWithTag("CameraPC");

        if (camGO  != null)
        {
            cam = camGO.GetComponent<Camera>();
        }
    }

    void Update()
    {
        if(cam != null)
            transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
    }
}
