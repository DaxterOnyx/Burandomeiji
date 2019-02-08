using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFacingPC : MonoBehaviour {

    GameObject camGO;
    Camera cam;

    void Update()
    {
        if(camGO == null)
        {
            camGO = GameObject.FindGameObjectWithTag("CameraPC");
        }
        else
        {
            if(cam == null)
            {
                cam = camGO.GetComponent<Camera>();
            }
            else
            {
                transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
            }
        }
    }
}
