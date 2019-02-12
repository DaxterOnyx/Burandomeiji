class ElectricityEffect : ElementEffect
{
	protected override void StartEffect()
	{
		base.StartEffect();
		TakeHits.Freeze();
	}
	protected override void Effect()
	{
		//DO nothing;
		TakeHits.takeHits(Damage, false);
	}
	protected override void StopEffect()
	{
		base.StopEffect();
		TakeHits.UnFreeze();
	}
}
