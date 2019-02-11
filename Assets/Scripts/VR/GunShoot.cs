using UnityEngine;
using VRTK;

/// <summary>
/// Script à mettre sur le pistolet
/// @author Brice
/// </summary>
public class GunShoot : WeaponScript
{
	public float RateOfFire = 1;
	private float TimeLastFire;
	private VRTK_ControllerReference Controller;
	public GameObject Explosion;
	public float ForceHaptic;
	public float DurationHaptic = 0.01f;

	private void Awake()
	{
		TimeLastFire = RateOfFire;
		Controller = VRTK.VRTK_ControllerReference.GetControllerReference(GetComponentInParent<HandVRControl>().gameObject);
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
		Instantiate(Explosion, projectileSpawnPoint.position, projectileSpawnPoint.rotation, projectileSpawnPoint);
		
		if(!VRTK_ControllerReference.IsValid(Controller))
			Controller = VRTK_ControllerReference.GetControllerReference(GetComponentInParent<VRTK_TrackedController>().gameObject);
		VRTK.VRTK_ControllerHaptics.TriggerHapticPulse(Controller,ForceHaptic,DurationHaptic,0.01f);
		base.Shoot();
	}
}
