using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeHits : MonoBehaviour {
    public int HitPoints = 3;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter");

        if (collision.collider.GetComponent<AIScript>() != null)
        {
            HitPoints--;
            Debug.Log("HitPoints");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");

        if (other.GetComponent<AIScript>() != null)
        {
            HitPoints--;
            Debug.Log("HitPoints");
        }
    }
}
