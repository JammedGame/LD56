

using System.Linq;
using Night;
using UnityEngine;

namespace DefaultNamespace.Spells
{
    public class LightningStrikeSpell : SpellBattleInstance
    {
        private const float BaseDamage = 5;
        
        private float damage;

        public override Vector2 BaseCastArea => new Vector2(1f, 1f);
        
        public override Vector2 CalculateCastArea(int level)
        {
            Vector2 castArea = BaseCastArea * (1 + (level - 1) * 0.2f);
            return castArea;
        }

        protected override void Init()
        {
            damage = BaseDamage * SpellLevel;
            
            Unit closestUnit = GetClosestUnitsInCastArea(CastTarget.ToVector2XZ()).FirstOrDefault();
            if (closestUnit != null)
            {
                closestUnit.DealDamage(damage, null);
            }
            
            Deactivate();
        }
    }
}