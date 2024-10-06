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
		
		public NightBattleContext(NightLevelData nightLevelData, UserBattleData userBattleData, Wall wall)
		{
			NightLevelData = nightLevelData;
			UserBattleData = userBattleData;
			MobSpawner = new MobSpawner(this, nightLevelData);
			Wall = wall;
			Wall.Setup(this, userBattleData.WallState.level, userBattleData.WallState.currentHealthNormalized);
			
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

			// Tick spell cooldowns
			foreach (UserEquippedSpell equippedSpell in UserBattleData.EquippedSpells)
			{
				equippedSpell.CooldownLeft -= Time.deltaTime;
			}

			CleanUpDeadObjects();
			Declump();
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
			
			spell.StartCooldown();
			SpellBattleInstance newSpell = Object.Instantiate(spell.Blueprint.SpellBattlePrefab, Wall.SquizzardPosition.position, Quaternion.identity);
			AllSpells.Add(newSpell);
			newSpell.Setup(this, castTargetPos, spell);

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

		public void Declump()
		{
			float declumpSpeed = Time.deltaTime * 3f;
			int unitsCount = AllUnits.Count;
			float declumpRadiusX = 2.5f;
			float declumpRadiusZ = 2.5f;
			
			for (int i = 0; i < unitsCount; i++)
			{
				Unit left = AllUnits[i];
				Vector3 leftPos = left.Position; 
				
				for (int j = i + 1; j < unitsCount; j++)
				{
					Unit right = AllUnits[j];
					Vector3 rightPos = right.Position;

					if (Mathf.Abs(leftPos.x - rightPos.x) >= declumpRadiusX
					    || Mathf.Abs(leftPos.z - rightPos.z) >= declumpRadiusZ)
					{
						continue;
					}

					Vector3 offset = rightPos.z > leftPos.z ? new Vector3(0, 0, 1f) : new Vector3(0, 0, -1f);

					float leftMultiplier = left.CurrentAction.AnimationId is UnitAnimationId.Attack ? 0.5f : 1f;
					float rightMultiplier = left.CurrentAction.AnimationId is UnitAnimationId.Attack ? 0.5f : 1f;
					left.Position -= declumpSpeed * leftMultiplier * offset;
					right.Position += declumpSpeed * rightMultiplier * offset;
				}
			}

			GameSettings settings = GameSettings.Instance;
			for (int i = 0; i < unitsCount; i++)
			{
				Unit left = AllUnits[i];
				Vector3 leftPos = left.Position;
				if (leftPos.x < settings.MinX
				    || leftPos.x > settings.MaxX
				    || leftPos.z < settings.MinZ
				    || leftPos.z > settings.MaxZ)
				{
					leftPos.x = Mathf.Clamp(leftPos.x, settings.MinX, settings.MaxX);
					leftPos.z = Mathf.Clamp(leftPos.z, settings.MinZ, settings.MaxZ);
					left.Position = leftPos;
				}
			}
		}
	}
}