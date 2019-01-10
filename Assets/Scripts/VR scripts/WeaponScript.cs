using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public abstract class WeaponScript : MonoBehaviour {
	public VRTK_ControllerEvents.ButtonAlias shotButton;

	public abstract void Use();

	private void Awake()
	{
		GetComponentInParent<VRTK_ControllerEvents>().SubscribeToButtonAliasEvent(shotButton, true, Use);
	}

	private void Use(object sender, ControllerInteractionEventArgs e)
	{
		//TODO not good if too much different weapon
		if(isActiveAndEnabled)
			Use();
	}
}
