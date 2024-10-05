using System;
using System.Collections.Generic;
using UnityEngine;

namespace Night
{
	[CreateAssetMenu]
	public class NightLevelData : ScriptableObject
	{
		public List<WaveSpawn> Spawns;
	}

	[Serializable]
	public class WaveSpawn
	{
		public float Time;
		public Unit UnitPrefab;
		public Vector3 SpawnPosition;
		public int UnitLevel;
	}
}