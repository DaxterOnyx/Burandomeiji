using UnityEngine;

public class GunShoot : WeaponScript
{

	public override void Use()
	{
		base.Use();
		FixedUpdate();
	}

	private void FixedUpdate()
	{
		if(inUse)
			Shoot();
	}
}
