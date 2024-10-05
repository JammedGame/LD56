using System;
using System.Collections.Generic;
using UnityEngine;

namespace Night
{
	[CreateAssetMenu]
	public class NightLevelData : ScriptableObject
	{
		// level design shit
		public List<WaveSpawn> Spawns;
	}

	[Serializable]
	public class WaveSpawn
	{
		// level design shit
	}
}