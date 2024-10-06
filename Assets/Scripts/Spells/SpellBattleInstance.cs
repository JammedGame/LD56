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
        protected NightBattleContext Context;
        protected Vector3 CastTarget { get; private set; }
        protected int SpellLevel { get; private set; }
        protected Vector2 AreaOfEffect { get; private set; }
        public bool IsActive { get; private set; }
        
        /// <summary>
        /// (0, 0) means point target
        /// </summary>
        public abstract Vector2 BaseCastArea { get; }

        public virtual Vector2 CalculateCastArea(int level) => Vector3.zero;
        
        public void Setup(NightBattleContext ctx, Vector3 target, int currentSpellLevel)
        {
            IsActive = true;
            Context = ctx;
            CastTarget = target;
            SpellLevel = currentSpellLevel;
            AreaOfEffect = CalculateCastArea(currentSpellLevel);
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