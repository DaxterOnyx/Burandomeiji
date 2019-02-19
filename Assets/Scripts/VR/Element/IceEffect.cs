using UnityEngine;

class IceEffect : ElementEffect
{
	[UnityEngine.Tooltip("Damage each second")]
	public float Damage = 5;

	protected override void StartEffect()
	{
		base.StartEffect();
		//TODO add graphic ice effect
		takeHitsEnemy.Slow(10);
		Debug.Log("IceEffect on "+ takeHitsEnemy);
	}
	protected override void Effect()
	{
		takeHitsEnemy.takeHits(Damage, false);
	}
	protected override void StopEffect()
	{
		base.StopEffect();
		//TODO remove graphic ice effect
		takeHitsEnemy.UnSlow();
	}
}
