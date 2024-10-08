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
		public WallState WallState;
	}

	public class UserUnitInfo
	{
		public GoodUnit MyUnit;
		public int level;

		public UserUnitInfo(GoodUnit myUnit, int level)
		{
			MyUnit = myUnit;
			this.level = level;
		}
	}
}