using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFacing : MonoBehaviour {

    [SerializeField]
    string name_tag_object;
    Transform tr;

    void Start()
    {
        tr = GameObject.FindGameObjectWithTag(name_tag_object).transform;
    }

    void Update()
    {
        transform.LookAt(transform.position + tr.rotation * Vector3.forward, tr.rotation * Vector3.up);
    }
}
