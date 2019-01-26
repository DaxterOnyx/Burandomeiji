using System;
using UnityEngine;

class ElementProjectile : ProjectileScript
{
	public float Power = 1;
	public GameObject ElementEffect;

	private void OnCollisionEnter(Collision collision)
	{
		var target = collision.collider.GetComponent<TakeHits>();
		if (target != null && target.tag == TargetTag)
		{
			target.takeHits(Damage);
			if (ElementEffect != null)
			{
				GameObject go = Instantiate(ElementEffect, target.transform);
				ElementEffect elementEffect = go.GetComponent<ElementEffect>();
				elementEffect.Power = Power;
				if(elementEffect is WindEffect)
				{
					((WindEffect)elementEffect).impacts = collision.contacts;
				}
			}
			Destroy(gameObject);
		}
	}
}

class ElementEffect : MonoBehaviour
{
	public float Power;

	private void FixedUpdate()
	{
		Power -= Time.fixedDeltaTime;
		Effect();
	}

	protected virtual void Effect()
	{ }
}