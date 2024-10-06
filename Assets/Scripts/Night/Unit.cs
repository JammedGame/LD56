using Night;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Night
{
    public abstract class Unit : MonoBehaviour
    {
        public float DeathAnimationDuration = 2f;
        
        // Stats
        public float Health;
        public float Speed;
        public float AttackRange => BaselineSettings.AttackRange;
        public float AgroRange => BaselineSettings.AgroRange;

        public abstract Team MyTeam { get; }
        public Animator Animator;
        public UnitTypeSettings BaselineSettings;
        public NightBattleContext BattleContext;

        public UnitCommandDecision CurrentAction { get; private set; }

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }
        public bool IsActive { get; private set; }

        public void Setup(NightBattleContext battleContext, int level)
        {
            BattleContext = battleContext;
            Health = BaselineSettings.Health; // apply levels?
            Speed = BaselineSettings.MoveSpeed; // apply levels?
            IsActive = true;
        }

        public void Tick()
        {
            if (!IsActive)
            {
                return;
            }
            
            CurrentAction = Think();

            switch (CurrentAction.AnimationId)
            {
                case UnitAnimationId.Attack:    DoAttack(CurrentAction); break;
                case UnitAnimationId.DoNothing: DoNothing(CurrentAction); break;
                case UnitAnimationId.Move:      DoMove(CurrentAction); break;
            }
        }

        private void DoMove(UnitCommandDecision currentAction)
        {
            Animator.SetTrigger("Move");

            if (currentAction.TargetUnit is Wall)
            {
                Position += Vector3.left * Speed * Time.deltaTime;
            }
            else
            {
                Position = Vector3.MoveTowards(Position, currentAction.TargetPosition, Speed * Time.deltaTime);
            }
        }

        private void DoNothing(UnitCommandDecision currentAction)
        {
        }

        private void DoAttack(UnitCommandDecision currentAction)
        {
            if (IsInAttackRange(currentAction.TargetUnit))
            {
                Animator.SetTrigger("Attack");
                // todo: damage
            }
            else
            {
                DoMove(currentAction);
            }
        }

        public bool IsInAttackRange(Unit targetUnit)
        {
            if (targetUnit is Wall wall)
            {
                return Mathf.Abs(Position.x - targetUnit.Position.x) < AttackRange;
            }
            else
            {
                return Vector3.Distance(Position, targetUnit.Position) < AttackRange;
            }
        }
        
        public abstract UnitCommandDecision Think();

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
            Animator.SetBool("Death", true);
            Destroy(gameObject, DeathAnimationDuration);
        }
    }
}

public enum Team
{
    Good,
    Bad
}

public struct UnitCommandDecision
{
    public UnitAnimationId AnimationId;
    public Unit TargetUnit;
    public Vector3 TargetPosition;
    
    public static UnitCommandDecision Idle()
    {
        return new UnitCommandDecision() { AnimationId = UnitAnimationId.DoNothing };
    }

    public static UnitCommandDecision Attack(Unit target)
    {
        return new UnitCommandDecision { AnimationId = UnitAnimationId.Attack, TargetUnit = target };
    }

    public static UnitCommandDecision MoveToPoint(Vector3 targetPosition)
    {
        return new UnitCommandDecision { AnimationId = UnitAnimationId.Move, TargetPosition = targetPosition };
    }    
}

public enum UnitAnimationId
{
    DoNothing,
    Attack,
    Move
}