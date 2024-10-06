namespace Night
{
	public class Wall : Unit
	{
		public override Team MyTeam => Team.Good;

		protected override void OnSpawn()
		{
		}

		public override UnitCommand Think()
		{
			return UnitCommand.Idle();
		}
	}
}