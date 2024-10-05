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
        
        public void Setup(NightBattleContext ctx, Vector3 target, int currentSpellLevel)
        {
            Context = ctx;
            CastTarget = target;
            SpellLevel = currentSpellLevel;
            Init();
        }

        protected abstract void Init();
        public abstract void Tick();
    }
}