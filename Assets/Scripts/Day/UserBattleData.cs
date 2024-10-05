using System;
using System.Collections.Generic;
using UnityEngine;

namespace Night
{
	[Serializable]
	public class UserBattleData
	{
		public List<UserUnitInfo> UserUnits = new();
		// sve sto je user setupovao
	}

	public class UserUnitInfo
	{
		public Vector3 PositionSpawn;
		public Unit UnitType;
		public int level;
	}
}