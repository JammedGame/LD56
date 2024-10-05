using System.Collections.Generic;
using DefaultNamespace.Spells;
using Night;
using UnityEngine;

namespace DefaultNamespace
{
	[CreateAssetMenu]
	public class GameSettings : ScriptableObject
	{
		public const float MaxInputRaycastDepth = 300f;
		public List<NightLevelData> Levels;
		public List<SpellBookItem> Spells;

		// Prefabs
		public PlayerBattleInputManager BattleInputManagerPrefab;
	}
}