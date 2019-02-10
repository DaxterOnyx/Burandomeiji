using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionControler : MonoBehaviour {
	private bool IsExploded = false;

	private void Update()
	{
		if (GetComponentInChildren<ParticleSystem>().particleCount != 0)
			IsExploded = true;
		if (IsExploded && GetComponentInChildren<ParticleSystem>().particleCount == 0 && !GetComponent<AudioSource>().isPlaying)
		{
			Debug.Log("explosion is dead");
			Destroy(gameObject);
		}
	}
}
