using System;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

/// <summary>
/// Script pour gérer une main et les objets avec laquelle elle intéragit
/// @author Brice
/// </summary>
public class HandVRControl : MonoBehaviour {

	List<Tool> Tools;
	public Tool ActiveWeapon { get;  private set;}

	// Use this for initialization
	void Start ()
	{
	}

	private void Awake()
	{
		Tools = new List<Tool>();
		foreach (var item in gameObject.GetComponentsInChildren<Tool>(true))
		{
			Tools.Add(item);
			item.gameObject.SetActive(false);
		}

		ChangeActiveWeapon(Tools[0]);
	}

	private void ChangeActiveWeapon(Tool p_tool)
	{
		if(p_tool == null)
		{
			Debug.LogError("new weapon is null");
			return;
		}
		if(ActiveWeapon != null)
		{
			ActiveWeapon.gameObject.SetActive(false);
		}
		ActiveWeapon = p_tool;
		ActiveWeapon.gameObject.SetActive(true);
		Debug.Log("Active Weapon is " + ActiveWeapon.gameObject);
	}

	internal void Use(object sender, ControllerInteractionEventArgs e)
	{
		ActiveWeapon.Use();
	}

	internal void EndUse(object sender, ControllerInteractionEventArgs e)
	{
		ActiveWeapon.EndUse();
	}

	internal void SwitchWeapon()
	{
		ChangeActiveWeapon(Tools[(Tools.IndexOf(ActiveWeapon)+1)%Tools.Count]);
	}
}
