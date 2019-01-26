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
	internal new GameObject ActiveProjectile {
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

	public float Power = 1;
	public float PowerStep = 1;
	public float PowerStart = 1;
	public float PowerEnd = 10;
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

		ChargingProjectile = Instantiate(ActiveProjectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
		ChargingProjectile.transform.localScale = new Vector3(Power, Power, Power);
	}

	public override void EndUse()
	{
		base.EndUse();
		
		Rigidbody projectileRigidbody = ChargingProjectile.GetComponent<Rigidbody>();
		if (projectileRigidbody != null)
		{
			projectileRigidbody.AddForce(ChargingProjectile.transform.forward * projectileSpeed);
		}
		ChargingProjectile = null;
	}

	private void FixedUpdate()
	{
		if(inUse)
		{
			Power += PowerStep * Time.fixedDeltaTime;
			ChargingProjectile.transform.localScale = new Vector3(Power, Power, Power);
		}
	}

	internal void ChangeElement(Element p_element)
	{
		m_element = p_element;
	}
}