using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public abstract class WeaponScript : Tool
{
    public float projectileSpeed = 1000f;
	public Transform projectileSpawnPoint;
	public GameObject Projectile;

	public virtual void Shoot()
	{
		if (Projectile != null && projectileSpawnPoint != null)
		{
			GameObject clonedProjectile = Instantiate(Projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
			Rigidbody projectileRigidbody = clonedProjectile.GetComponent<Rigidbody>();
			if (projectileRigidbody != null)
			{
				projectileRigidbody.AddForce(clonedProjectile.transform.forward * projectileSpeed);
			}
		}
	}
}

public abstract class Tool : MonoBehaviour
{
	protected bool inUse = false;
	public virtual void Use()
	{
		inUse = true;
	}

	public virtual void EndUse()
	{
		inUse = false;
	}
}