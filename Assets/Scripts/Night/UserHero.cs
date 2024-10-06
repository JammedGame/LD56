namespace Night
{
	public class UserHero : Unit
	{
		public override Team MyTeam => Team.Good;
		
		public override UnitCommandDecision Think()
		{
			return UnitCommandDecision.Idle();
		}
	}
}