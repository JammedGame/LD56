using UnityEngine;

namespace Night
{
	public class GoodUnit : Unit
	{
		public override Team MyTeam => Team.Good;

		protected override void OnSpawn()
		{
		}

		public override UnitCommand Think()
		{
			// continue attacking
			if (CurrentAction.AnimationId is UnitAnimationId.Attack && IsInAttackRange(CurrentAction.TargetUnit, 2f))
			{
				return UnitCommand.Attack(CurrentAction.TargetUnit); 
			}
			
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
				return UnitCommand.MoveAttack(this, aggroUnit);
			}
			
			return UnitCommand.MoveToPoint(MySpawnLocation);
		}
	}
}