using System;
using UnityEngine;

class WindEffect : ElementEffect
{
	internal Vector3 impact;
	[Tooltip("Force of the windn, will attract the targets at the impact")]
	public float WindForce = 10;

	protected override void StartEffect()
	{
		base.StartEffect();
		//TODO add Wind effect
		//TakeHits.GetComponent<Rigidbody>().AddForce(WindForce,ForceMode.Acceleration);
	}
	protected override void Effect()
	{
		//DO nothing; or increase force
		TakeHits.transform.position = Vector3.MoveTowards(TakeHits.transform.position, impact, WindForce * Time.deltaTime);
	}
	protected override void StopEffect()
	{
		base.StopEffect();
		//TODO remove wind effect
	}

	internal void Eye(Vector3 position)
	{
		if(impact == null)
		{
			impact = position;
		}
		else
		{
			impact = new Vector3(position.x + impact.x/2, position.y + impact.y/2, position.z + impact.z/2);
		}
	}
}

