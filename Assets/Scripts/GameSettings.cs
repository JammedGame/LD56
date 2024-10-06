using System.Collections.Generic;
using DefaultNamespace.Spells;
using Night;
using UnityEngine;

namespace DefaultNamespace
{
	[CreateAssetMenu]
	public class GameSettings : ScriptableObject
	{
		static GameSettings instance;
		public static GameSettings Instance
		{
			get
			{
				if (instance == null)
				{
					instance = Resources.Load<GameSettings>("Config/GameSettings");
				}

				return instance;
			}
		}
		
		public const float MaxInputRaycastDepth = 300f;
		public List<NightLevelData> Levels;
		public List<SpellBookItem> Spells;

		// Prefabs
		public PlayerBattleInputManager BattleInputManagerPrefab;

		public List<Vector3> GoodUnitSpawnLocations;
		public GoodUnit TestSpawn;

		[Header("Bounds")]
		public float MinX = -15f;
		public float MaxX = 15f;
		public float MinZ = -15f;
		public float MaxZ = 15;
	}
}