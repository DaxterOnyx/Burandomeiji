using System;
using System.Collections;
using UnityEngine;

class ElementProjectile : ProjectileScript
{
	public float Power = 1;
	public GameObject ElementEffect;
	public float multiplierScaleExplosion = 5f;
	public GameObject ElementFX;
	bool IsLaunched = false;
	public float FXMin = 1;
	public float FXMax = 10;
	private bool IsLanded = false;
	public AudioSource AudioSource;
	public AudioClip InLoading;
	public AudioClip FullLoaded;
	public AudioClip InFly;
	public float ExplosionPowerValue = 4;

	protected override void Awake()
	{
		GetComponent<Rigidbody>().useGravity = false;
		AudioSource.clip = InLoading;
		AudioSource.Play();
	}

	internal void Launch(float speed)
	{
		IsLaunched = true;
		Destroy(gameObject, LifeTime);
		transform.SetParent(null);
		GetComponentInChildren<Collider>(true).enabled = true;
		GetComponent<Rigidbody>().AddForce(transform.forward * speed,ForceMode.VelocityChange);
		GetComponent<Rigidbody>().useGravity = true;

		AudioSource.Stop();
		AudioSource.clip = InFly;
		AudioSource.loop = true;
		AudioSource.Play();
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (ElementEffect == null) { Debug.LogError("ElementEffect is not define in projectile"); return; }
		if (collision.collider.tag == "Player" || !IsLaunched) { return; }

		if(Power < ExplosionPowerValue)
		{
			//no explosion
			if (collision.gameObject.tag == TargetTag)
				AddEffect(collision.gameObject);
		}
		else
		{
			//Explosion

			//Create Explosion on Impact
			Vector3 explosionPos = transform.position;
			GameObject l_elementFX = Instantiate(ElementFX, explosionPos, Quaternion.identity);

			//set effect on ennemy in zone
			foreach (Collider hit in Physics.OverlapSphere(position: explosionPos, radius: multiplierScaleExplosion))
			{
				if (hit.tag == TargetTag)
				{
					AddEffect(hit.gameObject);
				}
			}
		}


		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		GetComponentInChildren<Collider>().enabled = false;
		foreach(var a in GetComponentsInChildren<ParticleSystem>()) a.Stop();
		IsLanded = true;
	}

	private void AddEffect(GameObject hit)
	{
		ElementEffect elementEffect = null;
		System.Type type = ElementEffect.GetComponent<ElementEffect>().GetType();
		ElementEffect[] elements = hit.GetComponentsInChildren<ElementEffect>();
		for (int i = 0; elementEffect == null && i < elements.Length; i++)
		{
			if (elements[i].GetType() == type)
			{
				elementEffect = elements[i];
			}
		}

		if (elementEffect == null)
		{
			Debug.Log("add effect " + ElementEffect + " sur " + hit);
			GameObject newEllementEffect = Instantiate(ElementEffect, hit.transform.position, Quaternion.identity, hit.transform);
			elementEffect = newEllementEffect.GetComponent<ElementEffect>();
		}
		if (elementEffect is WindEffect)
		{
			((WindEffect)elementEffect).Eye(transform.position);
            //TODO add graphic effect of wind TORNADO
        }
		elementEffect.ActiveEffect(Power);
	}

	private void Update()
	{
		if (IsLaunched && IsLanded && GetComponentInChildren<ParticleSystem>().particleCount == 0)
		{
			Debug.Log("Destroy inactive particle : " + this);
			Destroy(gameObject);
		}
	}

	internal void FullCharged()
	{
		//TODO Brice c'est moche, quoi que ?
		AudioSource.Stop();
		AudioSource.clip = FullLoaded;
		AudioSource.loop = true;
		AudioSource.Play();
	}
}

abstract class ElementEffect : MonoBehaviour
{
	protected float Power;
	protected TakeHits TakeHits;
	public float Cooldown = 0.2f;
	[Range(0.1f,1),Tooltip("Speed of decrement, \n" +
		"1 is very speed (1 of power was consume by callEffect),\n" +
		"0.1 is very long (0.1 of Power was consume by callEffect)")]
	public float Speed = 1;

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
			//if (takeHits.die())
			//{
			//	Destroy(gameObject);
			//	break;
			//}
			Effect();
			yield return new WaitForSeconds(Cooldown);
			Power-=Speed;
		}
		Power = 0;
		StopEffect();
	}

	protected virtual void StartEffect()
	{
		foreach(var ps in GetComponentsInChildren<ParticleSystem>())
			ps.Play();

	}
	protected abstract void Effect();
	protected virtual void StopEffect()
	{
		foreach(var ps in GetComponentsInChildren<ParticleSystem>())
			ps.Stop();
	}
}