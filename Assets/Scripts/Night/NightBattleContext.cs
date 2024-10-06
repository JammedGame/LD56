using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Spells;
using UnityEngine;

namespace Night
{
	public class NightBattleContext
	{
		public readonly NightLevelData NightLevelData;
		public readonly UserBattleData UserBattleData;
		public readonly Wall Wall;

		// battle objects
		public readonly List<Unit> AllUnits = new List<Unit>();
		public readonly HashSet<SpellBattleInstance> AllSpells = new HashSet<SpellBattleInstance>();
		public readonly MobSpawner MobSpawner;
		public float GameTime { get; private set; }
		public Team? Winner { get; private set; }

		private readonly Dictionary<string, UserEquippedSpell> spellMap = new Dictionary<string, UserEquippedSpell>();

		public NightBattleContext(NightLevelData nightLevelData, UserBattleData userBattleData, Wall wall)
		{
			NightLevelData = nightLevelData;
			UserBattleData = userBattleData;
			MobSpawner = new MobSpawner(this, nightLevelData);
			Wall = wall;
			Wall.Setup(this, userBattleData.WallState.level, userBattleData.WallState.currentHealthNormalized);

			foreach (UserEquippedSpell spell in UserBattleData.EquippedSpells)
			{
				spellMap[spell.Blueprint.Id] = spell;
			}

			// spawn user's units
			for (int i = 0; i < UserBattleData.UserUnits.Count; i++)
			{
				UserUnitInfo unitInfo = UserBattleData.UserUnits[i];
				Spawn(unitInfo.MyUnit, GameSettings.Instance.GoodUnitSpawnLocations[i], unitInfo.level);
			}
		}

		public Unit Spawn(Unit unitPrefab, Vector3 position, int level)
		{
			Unit newInstance = Unit.Instantiate(unitPrefab, position, Quaternion.identity);
			newInstance.Setup(this, level);
			AllUnits.Add(newInstance);
			return newInstance;
		}

		public void TickBattle()
		{
			MobSpawner.Tick();

			foreach (Unit unit in AllUnits)
			{
				unit.Tick();
			}

			foreach (SpellBattleInstance spellInstance in AllSpells)
			{
				spellInstance.Tick();
			}

			CleanUpDeadObjects();
			CheckWinner();

			GameTime += Time.deltaTime;
		}

		private void CleanUpDeadObjects()
		{
			AllSpells.RemoveWhere(x => !x.IsActive);
			AllUnits.RemoveAll(x => !x.IsActive);
		}
		
		public void CastSpell(UserEquippedSpell spell, Vector3 castTargetPos)
		{
			if (!Wall.IsAlive())
			{
				return;
			}
			
			SpellBattleInstance newSpell = Object.Instantiate(spell.Blueprint.SpellBattlePrefab, Wall.SquizzardPosition.position, Quaternion.identity);
			newSpell.Setup(this, castTargetPos, spell.Level);
			AllSpells.Add(newSpell);

			// Debug.Log($"Cast spell '{spellId}' level {equippedSpell.Level}, startPos {newSpell.transform.position}, castTarget {castTargetPos}");
		}
		
		public void CheckWinner()
		{
			if (Winner.HasValue)
			{
				return;
			}
			
			if (!Wall.IsAlive())
			{
				Winner = Team.Bad;
			}
			else if (MobSpawner.AllMobsDead())
			{
				Winner = Team.Good;
			}
		}
	}
}