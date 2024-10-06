using System.Collections.Generic;
using System.Linq;
using Night;
using UnityEngine;

namespace DefaultNamespace.Spells
{
    /// <summary>
    /// Instantiated when the spell is cast during a battle
    /// </summary>
    public abstract class SpellBattleInstance : MonoBehaviour
    {
        private UserEquippedSpell spellInfo;
        
        protected int SpellLevel => spellInfo.Level;
        protected NightBattleContext Context;
        protected Vector3 CastTarget { get; private set; }
        protected Vector2 AreaOfEffect { get; private set; }
        protected float Damage { get; private set; }
        public bool IsActive { get; private set; }

        public virtual Vector2 CalculateCastArea(int level, Vector2 baseCastArea)
        {
            Vector2 castArea = baseCastArea * (1 + (level - 1) * 0.2f);
            return castArea;
        }
        
        public virtual float CalculateDamage(int level, float baseDamage)
        {
            return baseDamage * (1 + (level - 1));
        }
        
        public void Setup(NightBattleContext ctx, Vector3 target, UserEquippedSpell spell)
        {
            spellInfo = spell;
            
            IsActive = true;
            Context = ctx;
            CastTarget = target;
            AreaOfEffect = CalculateCastArea(spell.Level, spell.CastArea);
            Damage = CalculateDamage(spell.Level, spell.Damage);
            Init();
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

        protected virtual void OnDeactivate()
        {
            Destroy(gameObject);
        }

        protected abstract void Init();

        public virtual void Tick()
        {
        }

        protected List<Unit> GetClosestUnitsInCastArea(Vector2 areaCenter)
        {
            return Context.AllUnits
                .Where(unit =>
                    MathUtils.IsPointInsideEllipse(areaCenter, AreaOfEffect, unit.transform.position.ToVector2XZ()))
                .OrderBy(unit => Vector3.Distance(unit.transform.position.ToVector2XZ(), areaCenter))
                .ToList();
        }
    }
}