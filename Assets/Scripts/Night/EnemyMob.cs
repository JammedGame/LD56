using UnityEngine;

namespace Night
{
	public class EnemyMob : Unit
	{
		public int LootAmount;
		
		public override Team MyTeam => Team.Bad;

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

			if (BattleContext.Wall.IsAlive())
			{
				return UnitCommand.MoveAttack(this, BattleContext.Wall);
			}
			
			return UnitCommand.Idle();
		}

		public override void OnDeactivate()
		{
			base.OnDeactivate();
			UserState.Instance.Gold.Add(LootAmount);
		}
	}

	public static class UnitUtil
	{
		public static bool IsAlive(this Unit unit)
		{
			return unit != null && unit.IsActive;
		}
	}
}