using UnityEngine;

namespace Night
{
	public class Unit : MonoBehaviour
	{
		public float Health;
		public float Speed;

		public UnitTypeSettings MySettings;
		public NightBattleContext BattleContext;

		public static Unit Spawn(NightBattleContext battleContext,
		                         UnitTypeSettings unitTypeSettings,
		                         Vector3 position,
		                         int level)
		{
			Unit newInstance = Instantiate(unitTypeSettings.Prefab, position, Quaternion.identity);
			newInstance.MySettings = unitTypeSettings;
			newInstance.BattleContext = battleContext;
			newInstance.Health = unitTypeSettings.Health; // apply levels?
			newInstance.Speed = unitTypeSettings.Speed; // apply levels?
			return newInstance;
		}

		public virtual void Tick()
		{
		}
	}
}