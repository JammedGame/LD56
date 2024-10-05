using System.Collections.Generic;
using UnityEngine;

namespace Night
{
	public class UserBattleData
	{
		public List<UserUnitInfo> UserUnits;
		// sve sto je user setupovao
	}

	public class UserUnitInfo
	{
		public Vector3 PositionSpawn;
		public UnitTypeSettings UnitType;
		public int level;
	}
}