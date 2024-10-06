﻿using System.Collections.Generic;
using Night;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Night
{
    public abstract class Unit : MonoBehaviour
    {
        public float DeathAnimationDuration = 2f;
        public float SpawnAnimationDuration = 0f;

        private float baseSpeed;
        
        // Stats
        public float Health;

        public float Speed
        {
            get
            {
                float finalSpeed = baseSpeed;
                foreach (UnitModifier modifier in ActiveModifiers)
                {
                    finalSpeed *= modifier.MoveSpeedMod;
                }

                return finalSpeed;
            }
        }
        
        public float AttackDamage;
        public float AttackRange => BaselineSettings.AttackRange;
        public float AgroRange => BaselineSettings.AgroRange;

        public abstract Team MyTeam { get; }
        public Animator Animator;
        public UnitTypeSettings BaselineSettings;
        public NightBattleContext BattleContext;

        public float MySpawnTime { get; private set; }
        public float StartHealth { get; private set; }
        public Vector3 MySpawnLocation { get; private set; }
        public UnitCommand CurrentAction { get; private set; }

        public float HealthPercent => Mathf.Clamp01(Health / StartHealth);

        public readonly List<UnitModifier> ActiveModifiers = new List<UnitModifier>();
        private readonly Dictionary<SpriteRenderer, Color> originalColors = new Dictionary<SpriteRenderer, Color>();

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }
        public bool IsActive { get; private set; }

        public void Setup(NightBattleContext battleContext, int level, float currentHealthNormalized = 1f)
        {
            BattleContext = battleContext;
            Health = BaselineSettings.Health * currentHealthNormalized; // apply levels?
            baseSpeed = BaselineSettings.MoveSpeed; // apply levels?
            AttackDamage = BaselineSettings.AttackDamage; // apply levels?
            
            // record spawn stuff
            MySpawnTime = BattleContext.GameTime;
            MySpawnLocation = Position;
            StartHealth = Health;
            
            IsActive = true;
            
            foreach (SpriteRenderer spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
            {
                originalColors[spriteRenderer] = spriteRenderer.color;
            }
            
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

            int i = 0;
            while (i < ActiveModifiers.Count)
            {
                if (ActiveModifiers[i].ShouldRemove())
                {
                    Debug.Log("Remove stats mod");
                    ActiveModifiers.RemoveAt(i);
                    continue;
                }

                i++;
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
            Animator.SetBool("Attack", false);

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
            Animator.SetBool("Attack", false);
        }

        private void DoAttack(UnitCommand currentAction)
        {
            Animator.SetBool("Attack", true);
        }

        public bool IsInAttackRange(Unit targetUnit, float padding = 0f)
        {
            if (!targetUnit.IsAlive())
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
        
        public void SetColorTint(Color tint)
        {
            foreach (SpriteRenderer spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
            {
                Color baseColor = Color.white;
                if (originalColors.TryGetValue(spriteRenderer, out Color color))
                {
                    baseColor = color;
                }
                
                spriteRenderer.color = baseColor * tint;
            }
        }

        public void RemoveColorTint()
        {
            if (this == null)
            {
                return;
            }
            
            foreach (SpriteRenderer spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
            {
                if (originalColors.TryGetValue(spriteRenderer, out Color originalColor))
                {
                    spriteRenderer.color = originalColor;
                }
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
        return new UnitCommand { AnimationId = UnitAnimationId.Attack, TargetUnit = target, TargetPosition = target.Position };
    }

    public static UnitCommand MoveAttack(Unit unit, Unit target)
    {
        if (unit.IsInAttackRange(target))
        {
            return Attack(target);
        }
        else
        {
            return MoveToPoint(target.Position);
        }
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