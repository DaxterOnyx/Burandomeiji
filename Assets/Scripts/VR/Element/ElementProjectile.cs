using System.Collections;
using UnityEngine;

class ElementProjectile : ProjectileScript
{
	public float Power = 1;
	public GameObject ElementEffect;
	public static float multiplierScaleExplosion = 10f;
	private bool inEffect;

	public float MultiplierScaleExplosion { get { return multiplierScaleExplosion; } set { multiplierScaleExplosion = value; } }
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

	private void OnTriggerStay(Collider collider)
	{
		if(inEffect)
		{
			if (collider.tag != TargetTag) { return; }
			Debug.Log(collider.tag);

			var ellementEffect = collider.GetComponentInChildren<ElementEffect>();
			//Debug.Log(t.GetType() + ""  + ElementEffect.GetType());
			if (ellementEffect != null && ellementEffect.GetType() == ElementEffect.GetType())
			{

			}
			else
			{
				Debug.Log("add effect " + ElementEffect + " sur " + collider);
				GameObject newEllementEffect = Instantiate(ElementEffect, collider.transform);
				ElementEffect elementEffect = newEllementEffect.GetComponent<ElementEffect>();
				if (elementEffect is WindEffect)
				{
					((WindEffect)elementEffect).impact = transform.position;
					//TODO add graphic effect of wind TORNADO
				}
				elementEffect.ActiveEffect(Power);
			}

		}
			
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (ElementEffect == null) { Debug.LogError("ElementEffect is not define in projectile"); return; }
		if (collision.collider.tag == "Player") { return; }

		//Create Explosion on Impact
		Vector3 explosionPos = transform.position;
		transform.localScale = Vector3.one * (multiplierScaleExplosion * Power);

		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
		GetComponentInChildren<Collider>().isTrigger = true;

		inEffect = true;
		//foreach (Collider hit in Physics.OverlapSphere(position: explosionPos, radius: multiplierScaleExplosion*Power))
		//{
		//	if (hit.tag == TargetTag)
		//	{
		//		GameObject go = Instantiate(ElementEffect, hit.transform);
		//		ElementEffect elementEffect = go.GetComponent<ElementEffect>();
		//		elementEffect.Power = Power;
		//		if (elementEffect is WindEffect)
		//		{
		//			((WindEffect)elementEffect).impact = explosionPos;
		//			//TODO add graphic effect of wind TORNADO
		//		}
		//	}
		//	else
		//		Debug.Log("hit object with tag = " + hit.tag); 
		//}
		Destroy(gameObject, Power);
	}
}

abstract class ElementEffect : MonoBehaviour
{
	protected float Power;
	protected TakeHits TakeHits;

	private void Awake()
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
		if (TakeHits == null)
			TakeHits = GetComponentInParent<TakeHits>();
		if (Power != -1)
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