using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {
	public float LifeTime = 5;
	public float Damage = 1;

	private void Awake()
	{
		Destroy(gameObject, LifeTime);
	}

	private void OnCollisionEnter(Collision collision)
	{
		//TODO collision 
	}
}
