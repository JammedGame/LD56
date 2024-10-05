using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Night
{
    public class Unit : MonoBehaviour
    {
        public float Health;
        public float Speed;

        public Animator Animator;
        public UnitTypeSettings BaselineSettings;
        public NightBattleContext BattleContext;

        [Space(10)]
        private Transform _target;
        [SerializeField] private LayerMask _targetLayers;

	public class Unit : MonoBehaviour
	{
		public float DeathAnimationDuration = 2f;
		public float Health;
		public float Speed;

		public Animator Animator;
		public UnitTypeSettings BaselineSettings;
		public NightBattleContext BattleContext;
		
		
		public bool IsActive { get; private set; }

		public static Unit Spawn(NightBattleContext battleContext,
		                         Unit unitPrefab,
		                         Vector3 position,
		                         int level)
		{
			Unit newInstance = Instantiate(unitPrefab, position, Quaternion.identity);
			newInstance.BattleContext = battleContext;
			newInstance.Health = unitPrefab.BaselineSettings.Health; // apply levels?
			newInstance.Speed = unitPrefab.BaselineSettings.Speed; // apply levels?
			newInstance.IsActive = true;
			return newInstance;
		}

        public static Unit Spawn(NightBattleContext battleContext,
                                 Unit unitPrefab,
                                 Vector3 position,
                                 int level)
        {
            Unit newInstance = Instantiate(unitPrefab, position, Quaternion.identity);
            newInstance.BattleContext = battleContext;
            newInstance.Health = unitPrefab.BaselineSettings.Health; // apply levels?
            newInstance.Speed = unitPrefab.BaselineSettings.MoveSpeed; // apply levels?
            return newInstance;
        }

        public virtual void Tick()
        {
            _target = SearchForTarget();

            if (_target == null) return;

            if (Vector3.Distance(transform.position, _target.position) > BaselineSettings.AttackRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, _target.position, Time.deltaTime * BaselineSettings.MoveSpeed);
            }
            else
            {
                Animator.SetTrigger("Attack");
            }
        }

        private Transform SearchForTarget()
        {
            Collider[] targets = Physics.OverlapSphere(transform.position, BaselineSettings.AgroRange, _targetLayers);
            if (targets.Length == 0) return null;
            return targets[0].transform;
        }
    }


			if (Input.GetKeyDown(KeyCode.Space))
			{
				Animator.SetTrigger("Attack");
			}
		}

		public void Deactivate()
		{
			if (!IsActive)
			{
				return;
			}

			IsActive = false;
			OnDeactivate();
		}

		public virtual void OnDeactivate()
		{
			Animator.SetTrigger("Death");
			Destroy(gameObject, DeathAnimationDuration);
		}
	}
}

public enum Team
{
    Good,
    Bad

}