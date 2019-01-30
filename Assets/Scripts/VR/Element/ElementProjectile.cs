using System;
using System.Collections;
using UnityEngine;

class ElementProjectile : ProjectileScript
{
	public float Power = 1;
	public GameObject ElementEffect;

	protected override void Awake()
	{
		GetComponent<Rigidbody>().useGravity = false;
	}

	internal void Launch(float speed)
	{
		Destroy(gameObject, LifeTime);
		transform.SetParent(null);
		GetComponent<Rigidbody>().AddForce(transform.forward * speed,ForceMode.VelocityChange);
		GetComponent<Rigidbody>().useGravity = true;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (ElementEffect == null) { Debug.LogError("ElementEffect is not define in projectile"); return; }

		//Create Explosion on Impact
		Vector3 explosionPos = transform.position;
		foreach (Collider hit in Physics.OverlapSphere(explosionPos, Power,100))
		{
			if(hit.tag == TargetTag)
			{
				GameObject go = Instantiate(ElementEffect, hit.transform);
				ElementEffect elementEffect = go.GetComponent<ElementEffect>();
				elementEffect.Power = Power;
				if(elementEffect is WindEffect)
				{
					((WindEffect)elementEffect).impact = explosionPos;
					//TODO add graphic effect of wind TORNADO
				}
			}
		}
		Destroy(gameObject);
	}
}

abstract class ElementEffect : MonoBehaviour
{
	public float Power;
	public TakeHits TakeHits;

	private void Start()
	{
		TakeHits = GetComponentInParent<TakeHits>();
		if (TakeHits == null)
		{
			Debug.LogError("Enemy without TakeHits script");
			Power = -1;
		}
	}

	internal void ActiveEffect(float p_Power)
	{
		if(Power != -1)
		{
			Power += p_Power;
			StartCoroutine(CallEffect());
		}
	}

	private IEnumerator CallEffect()
	{
		StartEffect();
		while (Power > 0)
		{
			Effect();
			yield return new WaitForSeconds(1f);
			Power--;
		}
		Power = 0;
		StopEffect();
	}

	protected abstract void StartEffect();
	protected abstract void Effect();
	protected abstract void StopEffect();
}