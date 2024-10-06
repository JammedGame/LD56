using DefaultNamespace.Spells;
using UnityEngine;

namespace Night
{
    public class UserEquippedSpell
    {
        public readonly SpellBookItem Blueprint;
        public readonly int Level;

        public Vector2 CastArea => Blueprint.SpellBattlePrefab.CalculateCastArea(Level);

        public UserEquippedSpell(SpellBookItem blueprint, int level)
        {
            Level = level;
            Blueprint = blueprint;
        }
    }
}