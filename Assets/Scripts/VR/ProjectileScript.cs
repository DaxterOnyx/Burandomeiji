using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// script à mettre sur tous les projectiles, gere le temps que le projectile reste dans le jeu si il ne touche personne et les dégats que fait le projectile à l'impact
/// </summary>
public class ProjectileScript : MonoBehaviour {
	public float LifeTime = 5;
	public float Damage = 1;
	public string TargetTag = "Ennemy";

	private void Awake()
	{
		Destroy(gameObject, LifeTime);
	}

	private void OnCollisionEnter(Collision collision)
	{
		//TODO Brice element différence
		var target = collision.collider.GetComponent<TakeHits>();
		if (target != null && target.tag == TargetTag)
		{
			target.takeHits(Damage);
			Destroy(gameObject);
		}
	}
}
