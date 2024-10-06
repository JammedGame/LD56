using System.Collections.Generic;
using UnityEngine;

namespace Night
{
	public class MobSpawner
	{
		public readonly NightBattleContext BattleContext;
		public readonly NightLevelData LevelData;

		private readonly List<WaveSpawn> waves;
		private float currentTime;

		public MobSpawner(NightBattleContext battleContext, NightLevelData levelData)
		{
			BattleContext = battleContext;
			LevelData = levelData;
			waves = new List<WaveSpawn>(levelData.Spawns);
		}

		public bool AllMobsDead()
		{
			return waves.Count == 0 && GetEnemiesRemainingCount() == 0; 
		}

		public void Tick()
		{
			currentTime += Time.deltaTime;

			for (int index = 0; index < waves.Count; index++)
			{
				WaveSpawn waveSpawn = waves[index];
				if (currentTime >= waveSpawn.Time)
				{
					BattleContext.Spawn(waveSpawn.UnitPrefab, waveSpawn.SpawnPosition, waveSpawn.UnitLevel);
					waves.RemoveAt(index--);
				}
			}
		}

		public int GetEnemiesRemainingCount()
		{
			int count = waves.Count;

			foreach(var unit in BattleContext.AllUnits)
			{
				if (unit.MyTeam == Team.Bad)
				{
					count++;
				}
			}

			return count;
		}
	}
}