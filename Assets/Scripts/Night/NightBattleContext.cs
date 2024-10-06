using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

		private readonly Dictionary<string, UserEquippedSpell> spellMap = new Dictionary<string, UserEquippedSpell>();
		
		public NightBattleContext(NightLevelData nightLevelData, UserBattleData userBattleData, Wall wall)
		{
			NightLevelData = nightLevelData;
			UserBattleData = userBattleData;
			MobSpawner = new MobSpawner(this, nightLevelData);
			Wall = wall;
			Wall.Setup(this, userBattleData.WallLevel);

			foreach (UserEquippedSpell spell in UserBattleData.EquippedSpells)
			{
				spellMap[spell.Blueprint.Id] = spell;
			}
			
			// spawn user's shit
			foreach (UserUnitInfo unitInfo in UserBattleData.UserUnits)
			{
				Spawn(unitInfo.UnitType, unitInfo.PositionSpawn, unitInfo.level);
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

			GameTime += Time.deltaTime;
		}

		private void CleanUpDeadObjects()
		{
			AllSpells.RemoveWhere(x => !x.IsActive);
			AllUnits.RemoveAll(x => !x.IsActive);
		}

		public void CastSpell(string spellId, Vector3 castTargetPos)
		{
			UserEquippedSpell equippedSpell = spellMap[spellId];
			SpellBattleInstance newSpell = Object.Instantiate(equippedSpell.Blueprint.SpellBattlePrefab);
			InputUtils.ScreenToWorld(new Vector2(0, Screen.height / 4f), out Vector3 spellStartPos);
			newSpell.transform.position = spellStartPos + Vector3.up * 5f;
			newSpell.Setup(this, castTargetPos, equippedSpell.Level);
			AllSpells.Add(newSpell);
			
			// Debug.Log($"Cast spell '{spellId}' level {equippedSpell.Level}, startPos {newSpell.transform.position}, castTarget {castTargetPos}");
		}
	}
}