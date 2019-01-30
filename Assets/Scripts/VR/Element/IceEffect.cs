class IceEffect : ElementEffect
{
	[UnityEngine.Tooltip("Damage each second")]
	public float Damage = 5;

	protected override void StartEffect()
	{
		//TODO add graphic ice effect
		//TakeHits.Slow();
	}
	protected override void Effect()
	{
		TakeHits.takeHits(Damage, false);
	}
	protected override void StopEffect()
	{
		//TODO remove graphic ice effect
		//TakeHits.UnSlow();
	}
}
