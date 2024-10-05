using UnityEngine;

namespace Night
{
	public class UnitTypeSettings : ScriptableObject
	{
		// baseline stats.
		public float Health;
		public float Speed;
		
		public Unit Prefab;
	}
}