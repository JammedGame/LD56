using UnityEngine;

namespace DefaultNamespace.Spells
{
    public abstract class SpellInstance : MonoBehaviour
    {
        public abstract void Setup(int currentSpellLevel);
        public abstract void Tick();
    }
}