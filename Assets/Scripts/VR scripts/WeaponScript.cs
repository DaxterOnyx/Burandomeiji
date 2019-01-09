using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	internal void Grab(bool v)
	{
		gameObject.GetComponent<VRTK.VRTK_InteractableObject>().Grabbed();
	}
}
