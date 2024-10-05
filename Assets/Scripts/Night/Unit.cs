using UnityEngine;

namespace Night
{
	public class Unit : MonoBehaviour
	{
		public float Health;
		public float Speed;

		public Animator Animator;
		public UnitTypeSettings BaselineSettings;
		public NightBattleContext BattleContext;

		public static Unit Spawn(NightBattleContext battleContext,
		                         Unit unitPrefab,
		                         Vector3 position,
		                         int level)
		{
			Unit newInstance = Instantiate(unitPrefab, position, Quaternion.identity);
			newInstance.BattleContext = battleContext;
			newInstance.Health = unitPrefab.BaselineSettings.Health; // apply levels?
			newInstance.Speed = unitPrefab.BaselineSettings.Speed; // apply levels?
			return newInstance;
		}

		public virtual void Tick()
		{
			transform.localPosition += Vector3.left * Time.deltaTime * Speed;

			if (Input.GetKeyDown(KeyCode.Space))
			{
				Animator.SetTrigger("Attack");
			}
		}
	}
}