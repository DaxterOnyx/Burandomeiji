using UnityEngine;

/// <summary>
/// Script à mettre sur le pistolet
/// @author Brice
/// </summary>
public class GunShoot : WeaponScript
{
	public float RateOfFire = 1;
	private float TimeLastFire;

	private void Awake()
	{
		TimeLastFire = RateOfFire;	
	}

	public override void Use()
	{
		Shoot();
		base.Use();
	}

	private void FixedUpdate()
	{
		TimeLastFire += Time.fixedDeltaTime;
		if (inUse && TimeLastFire >= RateOfFire)
		{
			Shoot();
		}
	}

	public override void Shoot()
	{
		TimeLastFire = 0;
		base.Shoot();
	}
}
