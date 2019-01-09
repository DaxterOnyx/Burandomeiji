using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVRControl : MonoBehaviour {

	List<GameObject> Weapons;
	public GameObject ActiveWeapon	{ get;  private set;}

	// Use this for initialization
	void Start () {
	}

	private void Awake()
	{
		Weapons = new List<GameObject>();
		foreach (var item in gameObject.GetComponentsInChildren<WeaponScript>(true))
		{
			Weapons.Add(item.gameObject);
			item.gameObject.SetActive(false);
		}

		ChangeActiveWeapon(Weapons[0]);
	}

	private void ChangeActiveWeapon(GameObject p_weapon)
	{
		if(gameObject == null)
		{
			UnityEngine.Debug.Log("new weapon is null");
			return;
		}
		ActiveWeapon.SetActive(false);
		ActiveWeapon.GetComponent<WeaponScript>().Grab(true);
		ActiveWeapon = p_weapon;
		ActiveWeapon.SetActive(true);
		ActiveWeapon.GetComponent<WeaponScript>().Grab(false);
	}


	// Update is called once per frame
	void Update () {
		
	}

	
}
