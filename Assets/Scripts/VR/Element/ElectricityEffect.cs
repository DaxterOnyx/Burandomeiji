class ElectricityEffect : ElementEffect
{
	protected override void StartEffect()
	{
		base.StartEffect();
		takeHitsEnemy.Freeze();
	}
	protected override void Effect()
	{
		//DO nothing;
		takeHitsEnemy.takeHits(Damage, false);
	}
	protected override void StopEffect()
	{
		base.StopEffect();
		takeHitsEnemy.UnFreeze();
	}
}
