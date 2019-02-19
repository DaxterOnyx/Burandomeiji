class FireEffect : ElementEffect
{
	[UnityEngine.Tooltip("Damage each second")]
	public float Damage = 10;
	
	protected override void StartEffect()
	{
		base.StartEffect();
		//TODO add graphic fire effect 
	}
	protected override void Effect()
	{
		takeHitsEnemy.takeHits(Damage, false);
	}
	protected override void StopEffect()
	{
		base.StopEffect();
		//TODO remove graphic fire effect
	}
}
