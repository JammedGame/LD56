using UnityEngine;

namespace Night
{
	public class Wall : Unit
	{
		public Transform SquizzardPosition;
		
		public override Team MyTeam => Team.Good;

		protected override void OnSpawn()
		{
		}

		public override UnitCommand Think()
		{
			return UnitCommand.Idle();
		}

		public override void OnDeactivate()
		{
			Destroy(gameObject);
		}
	}
}