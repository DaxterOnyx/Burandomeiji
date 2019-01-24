using System;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

/// <summary>
/// Script pour gérer une main et les objets avec laquelle elle intéragit
/// @author Brice
/// </summary>
public class HandVRControl : MonoBehaviour {

	List<WeaponScript> Weapons;
	public WeaponScript ActiveWeapon { get;  private set;}

	// Use this for initialization
	void Start ()
	{
	}

	private void Awake()
	{
		Weapons = new List<WeaponScript>();
		foreach (var item in gameObject.GetComponentsInChildren<WeaponScript>(true))
		{
			Weapons.Add(item);
			item.gameObject.SetActive(false);
		}

		ChangeActiveWeapon(Weapons[0]);
	}

	private void ChangeActiveWeapon(WeaponScript p_weapon)
	{
		if(p_weapon == null)
		{
			Debug.LogError("new weapon is null");
			return;
		}
		if(ActiveWeapon != null)
		{
			ActiveWeapon.gameObject.SetActive(false);
		}
		ActiveWeapon = p_weapon;
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
		ChangeActiveWeapon(Weapons[Weapons.IndexOf(ActiveWeapon)+1%Weapons.Count]);
	}
}
