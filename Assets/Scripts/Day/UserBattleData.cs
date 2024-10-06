using System;
using System.Collections.Generic;
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
		public GoodUnit MyUnit;
		public int level;
	}
}