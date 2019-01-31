using System;
using UnityEngine;

public class SpellBook : Tool
{
	public VRTK.VRTK_RadialMenu RadialMenu;
	public WandShoot Wand;
	private Element m_element;

	//public GameObject LeftPage;
	//public GameObject RightPage;
	//public float RightOpenAngle = 75f;
	//public float LeftOpenAngle = -75f;
	//public float RightCloseAngle = 0f;
	//public float LeftCloseAngle = 0f;
	//private bool Open { get { return LeftPage.transform.localRotation.y >= LeftOpenAngle && RightPage.transform.localRotation.y <= RightOpenAngle; } }
	//private bool Close { get { return LeftPage.transform.localRotation.y <= LeftCloseAngle && RightPage.transform.localRotation.y >= RightCloseAngle; } }

	private void Awake()
	{
		if(RadialMenu == null)
			RadialMenu = GetComponentInChildren<VRTK.VRTK_RadialMenu>();
		if (RadialMenu == null)
			Debug.LogError("RadialMenu on SpellBook is unfindable");

		if (Wand == null)
			Wand = GetComponentInParent<PlayerVRControl>().GetComponentInChildren<WandShoot>();
		if (Wand == null)
		{
			Debug.LogError("Wand on SpellBook is unfindable maibe inactive");
		}
	}

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

	public void FixedUpdate()
	{
		//if (inUse)
		//{
		//	if (Open)
		//	{
		//		LeftPage.transform.localRotation = new Quaternion(0, LeftOpenAngle, 0, 0);
		//		RightPage.transform.localRotation = new Quaternion(0, RightOpenAngle, 0, 0);
		//	}
		//	else
		//	{
		//		LeftPage.transform.Rotate(0, 1, 0);
		//		RightPage.transform.Rotate(0, -1, 0);
		//	}
		//}
		//else
		//{
		//	if (Close)
		//	{
		//		LeftPage.transform.localRotation = new Quaternion(0, LeftCloseAngle, 0, 0);
		//		RightPage.transform.localRotation = new Quaternion(0, RightCloseAngle, 0, 0);
		//	}
		//	else
		//	{
		//		LeftPage.transform.Rotate(0, -1, 0);
		//		RightPage.transform.Rotate(0, 1, 0);
		//	}
		//}
	}

	private void ChangeElement(Element p_element)
	{
		m_element = p_element;
		Wand.ChangeElement(p_element);
		Debug.Log("Element = " + m_element);
	}

	public void IceChange()
	{
		ChangeElement(Element.Ice);
	}

	public void WindChange()
	{
		ChangeElement(Element.Wind);
	}

	public void FireChange()
	{
		ChangeElement(Element.Fire);
	}

	public void ElectricityChange()
	{
		ChangeElement(Element.Electricity);
	}
}
