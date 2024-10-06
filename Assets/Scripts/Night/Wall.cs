namespace Night
{
	public class Wall : Unit
	{
		public override Team MyTeam => Team.Good;
		
		public override UnitCommandDecision Think()
		{
			return UnitCommandDecision.Idle();
		}
	}
}