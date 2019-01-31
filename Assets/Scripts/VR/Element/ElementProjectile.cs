using System;
using System.Collections;
using UnityEngine;

class ElementProjectile : ProjectileScript
{
	public float Power = 1;
	public GameObject ElementEffect;
	public static float multiplierScaleExplosion = 100;

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
		transform.localScale = Vector3.one * (100 * Power);
		Debug.Log(Power+" -> " + transform.localScale);


		GetComponent<Rigidbody>().isKinematic = true;
		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
		GetComponent<Rigidbody>().Sleep();
		GetComponentInChildren<Collider>().enabled = false;

		foreach (Collider hit in Physics.OverlapSphere(position: explosionPos, radius: 100*Power))
		{
			if (hit.tag == TargetTag)
			{
				GameObject go = Instantiate(ElementEffect, hit.transform);
				ElementEffect elementEffect = go.GetComponent<ElementEffect>();
				elementEffect.Power = Power;
				if (elementEffect is WindEffect)
				{
					((WindEffect)elementEffect).impact = explosionPos;
					//TODO add graphic effect of wind TORNADO
				}
			}
		}
		Destroy(gameObject,0.5f);
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
			if (Power == 0)
			{
				Power = p_Power;
				StartCoroutine(CallEffect());
			}
			else
				Power += p_Power;
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