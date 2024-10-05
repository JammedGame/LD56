﻿using Night;
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
        public bool IsActive { get; private set; }
        
        public void Setup(NightBattleContext ctx, Vector3 target, int currentSpellLevel)
        {
            IsActive = true;
            Context = ctx;
            CastTarget = target;
            SpellLevel = currentSpellLevel;
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
        public abstract void Tick();
    }
}