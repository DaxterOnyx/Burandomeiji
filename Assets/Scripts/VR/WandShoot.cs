using System;
using UnityEngine;

enum Element
{
	Electricity,
	Fire,
	Ice,
	Wind
}

public class WandShoot : WeaponScript
{
	[HideInInspector]
	internal new GameObject ActiveProjectile
	{
		get
		{
			switch (m_element)
			{
				case Element.Electricity:
					return ElectricityProjectile;
				case Element.Fire:
					return FireProjectile;
				case Element.Ice:
					return IceProjectile;
				case Element.Wind:
					return WindProjectile;
				default:
					Debug.LogError("Element ERROR");
					return FireProjectile;
			}
		}
	}

	public GameObject FireProjectile;
	public GameObject ElectricityProjectile;
	public GameObject IceProjectile;
	public GameObject WindProjectile;
	[SerializeField]
	private Element m_element = Element.Fire;

	private float power = 1;
	public float Power
	{
		get { return power; }
		set {
			if (value > PowerEnd) power = PowerEnd;
			else if (value < PowerStart) power = PowerStart;
			else power = value;
		}
	}
	public float PowerScale { get { return Power / ScaleModifierProjectile; } }
	public float PowerChargingSpeed = 1;
	public float PowerStart = 1;
	public float PowerEnd = 10;
	public float ScaleModifierProjectile = 50f;
	private GameObject ChargingProjectile;

	public override void Use()
	{
		base.Use();
		Power = PowerStart;

		if (projectileSpawnPoint == null)
		{
			Debug.LogError("projectile Spawn is null");
			gameObject.SetActive(false);
		}

		ChargingProjectile = Instantiate(ActiveProjectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation, GetComponentInParent<HandVRControl>().transform);
		ChargingProjectile.transform.localScale = new Vector3(PowerScale, PowerScale, PowerScale);
	}

	public override void EndUse()
	{
		base.EndUse();
		ChargingProjectile.GetComponent<ElementProjectile>().Power = Power;
		ChargingProjectile.GetComponent<ElementProjectile>().Launch(projectileSpeed);
		ChargingProjectile = null;
	}

	private void FixedUpdate()
	{
		if(inUse)
		{
			Power += PowerChargingSpeed * Time.fixedDeltaTime;
			
			ChargingProjectile.transform.localScale = new Vector3(PowerScale, PowerScale, PowerScale);
		}
	}

	internal void ChangeElement(Element p_element)
	{
		m_element = p_element;
		Debug.Log("Element = " + m_element);
	}
}