using UnityEngine;

namespace Night
{
	public class UserHero : Unit
	{
		public override Team MyTeam => Team.Good;

		protected override void OnSpawn()
		{
		}

		public override UnitCommand Think()
		{
			// aggro units if possible.
			Unit aggroUnit = null;
			float aggroRange = AgroRange;
			foreach (Unit unit in BattleContext.AllUnits)
			{
				if (unit.MyTeam != MyTeam && Vector3.Distance(Position, unit.Position) is float dist && dist < aggroRange)
				{
					aggroRange = dist;
					aggroUnit = unit;
				}
			}

			if (aggroUnit != null)
			{
				return UnitCommand.Attack(aggroUnit);
			}
			
			return UnitCommand.Attack(BattleContext.Wall);
		}
	}
}