using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PlayerVRControl : MonoBehaviour {

	public HandVRControl LeftHand;
	public HandVRControl RightHand;
	public VRTK_ControllerEvents.ButtonAlias SwitchButton;


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

		LeftHand.GetComponent<VRTK.VRTK_ControllerEvents>().SubscribeToButtonAliasEvent(SwitchButton, true, Switch);
	}

	private void Switch(object sender, ControllerInteractionEventArgs e)
	{
		LeftHand.SwitchWeapon();
		RightHand.SwitchWeapon();
	}

	// Update is called once per frame
	void Update () {
		
	}
}
