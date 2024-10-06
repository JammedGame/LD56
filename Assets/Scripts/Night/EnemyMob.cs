namespace Night
{
	public class EnemyMob : Unit
	{
		public override Team MyTeam => Team.Bad;

		public override UnitCommandDecision Think()
		{
			// aggro units if possible.
			
			return UnitCommandDecision.Attack(BattleContext.Wall);
		}
	}
}