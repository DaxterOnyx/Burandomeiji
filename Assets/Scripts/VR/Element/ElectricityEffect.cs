class ElectricityEffect : ElementEffect
{
	protected override void StartEffect()
	{
		base.StartEffect();
		//TODO add graphic electricity effect
		TakeHits.Freeze();
	}
	protected override void Effect()
	{
		//DO nothing;
	}
	protected override void StopEffect()
	{
		base.StopEffect();
		//TODO remove graphic electricity effect
		TakeHits.UnFreeze();
	}
}
