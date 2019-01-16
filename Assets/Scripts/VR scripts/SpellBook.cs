using System;
using UnityEngine;

public class SpellBook : Tool
{
	public VRTK.VRTK_RadialMenu RadialMenu;

	private void Awake()
	{
		if(RadialMenu == null)
			RadialMenu = GetComponentInChildren<VRTK.VRTK_RadialMenu>();
		if (RadialMenu == null)
			Debug.LogError("RadialMenu on SpellBook is null");
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
