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
	protected override void Awake()
	{
		GetComponent<Rigidbody>().useGravity = false;
	}

	internal void Launch(float speed)
	{
		IsLaunched = true;
		Destroy(gameObject, LifeTime);
		transform.SetParent(null);
		GetComponentInChildren<Collider>(true).enabled = true;
		GetComponent<Rigidbody>().AddForce(transform.forward * speed,ForceMode.VelocityChange);
		GetComponent<Rigidbody>().useGravity = true;
	}

	private void OnCollisionEnter(Collision collision)
	{
		//TODO
		if (ElementEffect == null) { Debug.LogError("ElementEffect is not define in projectile"); return; }
		if (collision.collider.tag == "Player" || !IsLaunched) { return; }

		//Create Explosion on Impact
		Vector3 explosionPos = transform.position;
		GameObject l_elementFX = Instantiate(ElementFX, explosionPos, Quaternion.identity);

		//set effect on ennemy in zone
		foreach (Collider hit in Physics.OverlapSphere(position: explosionPos, radius: multiplierScaleExplosion))
		{
			if(hit.tag==TargetTag)
			{

				ElementEffect elementEffect = null;
				ElementEffect[] elements = collision.collider.GetComponentsInChildren<ElementEffect>();
				for (int i = 0; elementEffect == null && i < elements.Length; i++)
				{
					if (elements[i].GetType() == ElementEffect.GetType())
					{
						elementEffect = elements[i];
					}
				}

				if (!elementEffect)
				{
					Debug.Log("add effect " + ElementEffect + " sur " + hit);
					GameObject newEllementEffect = Instantiate(ElementEffect, Vector3.zero, Quaternion.identity, hit.transform);
					elementEffect = newEllementEffect.GetComponent<ElementEffect>();
				}
				if (elementEffect is WindEffect)
				{
					((WindEffect)elementEffect).Eye(transform.position);
					//TODO add graphic effect of wind TORNADO
				}
				elementEffect.ActiveEffect(Power);
			}
		}

		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		GetComponentInChildren<Collider>().enabled = false;
		foreach(var a in GetComponentsInChildren<ParticleSystem>()) a.Stop();
		IsLanded = true;
	}

	private void Update()
	{
		if (GetComponentInChildren<ParticleSystem>().particleCount == 0)
			Debug.Log("particle is dead");
		if (IsLaunched && IsLanded && GetComponentInChildren<ParticleSystem>().particleCount == 0)
		{
			Debug.Log("Destroy inactive particle : " + this);
			Destroy(gameObject);
		}
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