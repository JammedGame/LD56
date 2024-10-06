using System;
using System.Collections.Generic;
using UnityEngine;

namespace Night
{
	[CreateAssetMenu]
	public class NightLevelData : ScriptableObject
	{
		public float SurviveTimerSeconds = 120;
		
		[Table]
		[NonReorderable]
		public List<WaveSpawn> Spawns;
	}

	[Serializable]
	public class WaveSpawn
	{
		public float Time;
		public Unit UnitPrefab;
		public float X;
		public float Y;
		public int UnitLevel;
		public Vector3 SpawnPosition => new(X, 0, Y);
	}
}