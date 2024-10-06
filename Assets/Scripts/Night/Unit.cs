﻿using Night;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Night
{
    public abstract class Unit : MonoBehaviour
    {
        public float DeathAnimationDuration = 2f;
        public float SpawnAnimationDuration = 0f;
        
        // Stats
        public float Health;
        public float Speed;
        public float AttackDamage;
        public float AttackRange => BaselineSettings.AttackRange;
        public float AgroRange => BaselineSettings.AgroRange;

        public abstract Team MyTeam { get; }
        public Animator Animator;
        public UnitTypeSettings BaselineSettings;
        public NightBattleContext BattleContext;

        public float MySpawnTime { get; private set; }
        public Vector3 MySpawnLocation { get; private set; }
        public UnitCommand CurrentAction { get; private set; }

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
            AttackDamage = BaselineSettings.AttackDamage; // apply levels?
            MySpawnTime = BattleContext.GameTime;
            MySpawnLocation = Position;
            IsActive = true;
            OnSpawn();
        }

        protected abstract void OnSpawn();

        public void Tick()
        {
            if (!IsActive)
            {
                return;
            }

            // spawn animation delay.
            if (BattleContext.GameTime < MySpawnTime + SpawnAnimationDuration)
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

        private void DoMove(UnitCommand currentAction)
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

        private void DoNothing(UnitCommand currentAction)
        {
        }

        private void DoAttack(UnitCommand currentAction)
        {
            if (IsInAttackRange(currentAction.TargetUnit))
            {
                Animator.SetTrigger("Attack");
            }
            else
            {
                DoMove(currentAction);
            }
        }

        public bool IsInAttackRange(Unit targetUnit, float padding = 0f)
        {
            if (targetUnit is null or { IsActive: false })
            {
                return false;
            }
            
            if (targetUnit is Wall wall)
            {
                return Mathf.Abs(Position.x - targetUnit.Position.x) < AttackRange + padding;
            }
            else
            {
                return Vector3.Distance(Position, targetUnit.Position) < AttackRange + padding;
            }
        }
        
        public abstract UnitCommand Think();

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

        public void ZAttack()
        {
            Unit currentTarget = CurrentAction.TargetUnit;
            if (currentTarget.IsAlive())
            {
                currentTarget.DealDamage(AttackDamage, this);
            }
        }

        public void DealDamage(float attackDamage, Unit unit)
        {
            Health -= attackDamage;
            
            if (Health <= 0)
            {
                Deactivate();
            }
        }
    }
}

public enum Team
{
    Good,
    Bad
}

public struct UnitCommand
{
    public UnitAnimationId AnimationId;
    public Unit TargetUnit;
    public Vector3 TargetPosition;
    
    public static UnitCommand Idle()
    {
        return new UnitCommand() { AnimationId = UnitAnimationId.DoNothing };
    }

    public static UnitCommand Attack(Unit target)
    {
        return new UnitCommand { AnimationId = UnitAnimationId.Attack, TargetUnit = target };
    }

    public static UnitCommand MoveToPoint(Vector3 targetPosition)
    {
        return new UnitCommand { AnimationId = UnitAnimationId.Move, TargetPosition = targetPosition };
    }    
}

public enum UnitAnimationId
{
    DoNothing,
    Attack,
    Move
}