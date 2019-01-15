using System;
using UnityEngine;

public class SpellBook : WeaponScript
{
	public VRTK.VRTK_RadialMenu RadialMenu;

	private void Awake()
	{
		RadialMenu = GetComponentInChildren<VRTK.VRTK_RadialMenu>();
	}

	//TODO @Daxter
	public override void Use()
	{
		base.Use();
		RadialMenu.ShowMenu();
	}

	public override void EndUse()
	{
		base.EndUse();
		RadialMenu.HideMenu(true);
	}
}
