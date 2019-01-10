using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVRControl : MonoBehaviour {

	List<WeaponScript> Weapons;
	public WeaponScript ActiveWeapon	{ get;  private set;}

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
			UnityEngine.Debug.Log("new weapon is null");
			return;
		}
		if(ActiveWeapon != null)
		{
			ActiveWeapon.gameObject.SetActive(false);
		}
		ActiveWeapon = p_weapon;
		ActiveWeapon.gameObject.SetActive(true);

	}
}
