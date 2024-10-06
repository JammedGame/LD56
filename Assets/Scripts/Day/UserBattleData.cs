using System;
using System.Collections.Generic;
using DefaultNamespace.Spells;
using UnityEngine;

namespace Night
{
	[Serializable]
	public class UserBattleData
	{
		public List<UserUnitInfo> UserUnits = new();
		public List<UserEquippedSpell> EquippedSpells = new();
		public int WallLevel;
	}

	public class UserUnitInfo
	{
		public Vector3 PositionSpawn;
		public Unit UnitType;
		public int level;
	}

	public class UserEquippedSpell
	{
		public readonly SpellBookItem Blueprint;
		public readonly int Level;

		public UserEquippedSpell(SpellBookItem blueprint, int level)
		{
			Level = level;
			Blueprint = blueprint;
		}
	}
}