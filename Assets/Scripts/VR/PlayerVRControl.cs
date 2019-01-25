using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

/// <summary>
/// script pour gérer les évenement VR de chaque main
/// @author Brice
/// </summary>
public class PlayerVRControl : MonoBehaviour
{

	public HandVRControl LeftHand;
	public HandVRControl RightHand;
	public VRTK_ControllerEvents.ButtonAlias UseWeaponButton = VRTK_ControllerEvents.ButtonAlias.TriggerClick;
	public VRTK_ControllerEvents.ButtonAlias SwitchWeaponButton = VRTK_ControllerEvents.ButtonAlias.ButtonTwoPress;

	// Use this for initialization
	void Awake()
	{
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
		LeftHand.GetComponent<VRTK_ControllerEvents>().SubscribeToButtonAliasEvent(UseWeaponButton, false, LeftHand.EndUse);
		RightHand.GetComponent<VRTK_ControllerEvents>().SubscribeToButtonAliasEvent(UseWeaponButton, false, RightHand.EndUse);

		//switch weapon
		LeftHand.GetComponent<VRTK_ControllerEvents>().SubscribeToButtonAliasEvent(SwitchWeaponButton, true, Switch);
	}

	/// <summary>
	/// change les armes de chaque main
	/// </summary>
	private void Switch(object sender, ControllerInteractionEventArgs e)
	{
		LeftHand.SwitchWeapon();
		RightHand.SwitchWeapon();
	}
}
