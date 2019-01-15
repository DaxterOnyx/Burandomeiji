using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PlayerVRControl : MonoBehaviour {

	public HandVRControl LeftHand;
	public HandVRControl RightHand;
	public VRTK_ControllerEvents.ButtonAlias UseWeaponButton;


	// Use this for initialization
	void Awake () {
		if (LeftHand == null)
		{
			Debug.LogError("LeftHand is null");
			gameObject.SetActive(false);
		}
		if (RightHand == null)
		{
			Debug.LogError("RightHand is null");
			gameObject.SetActive(false);
		}
		
		//use weapon
		LeftHand.GetComponent<VRTK_ControllerEvents>().SubscribeToButtonAliasEvent(UseWeaponButton, true, LeftHand.Use);
		RightHand.GetComponent<VRTK_ControllerEvents>().SubscribeToButtonAliasEvent(UseWeaponButton, true, RightHand.Use);
	}

	public void Switch()
	{
		LeftHand.SwitchWeapon();
		RightHand.SwitchWeapon();
	}

	// Update is called once per frame
	void Update () {
		
	}
}
