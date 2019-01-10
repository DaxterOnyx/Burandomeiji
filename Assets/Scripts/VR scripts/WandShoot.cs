using UnityEngine;

public class WandShoot : WeaponScript
{
	//public new VRTK_ControllerEvents.ButtonAlias shotButton;
	public GameObject projectile;
	public Transform projectileSpawnPoint;
	public float projectileSpeed = 1000f;
	public float projectileLife = 5f;

	public override void Use()
	{
		if (projectile != null && projectileSpawnPoint != null)
		{
			GameObject clonedProjectile = Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
			Rigidbody projectileRigidbody = clonedProjectile.GetComponent<Rigidbody>();
			float destroyTime = 0f;
			if (projectileRigidbody != null)
			{
				projectileRigidbody.AddForce(clonedProjectile.transform.forward * projectileSpeed);
				destroyTime = projectileLife;
			}
			Destroy(clonedProjectile, destroyTime);
		}
	}
}