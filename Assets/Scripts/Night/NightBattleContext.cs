using System.Collections.Generic;
using UnityEngine;

namespace Night
{
	public class NightBattleContext
	{
		public readonly NightLevelData NightLevelData;
		public readonly UserBattleData UserBattleData;
		
		// battle objects
		public readonly List<Unit> AllUnits = new();
		
		public NightBattleContext(NightLevelData nightLevelData, UserBattleData userBattleData)
		{
			NightLevelData = nightLevelData;
			UserBattleData = userBattleData;

			// spawn user's shit
			foreach (UserUnitInfo unitInfo in UserBattleData.UserUnits)
			{
				Spawn(unitInfo.UnitType, unitInfo.PositionSpawn, unitInfo.level);
			}
		}

		public Unit Spawn(Unit unitPrefab, Vector3 position, int level)
		{
			Unit instance = Unit.Spawn(this, unitPrefab, position, level);
			AllUnits.Add(instance);
			return instance;
		}
		
		public void TickBattle()
		{
			foreach (Unit unit in AllUnits)
			{
				unit.Tick();
			}
		}
	}
}