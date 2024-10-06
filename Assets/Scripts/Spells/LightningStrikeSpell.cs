

using System.Linq;
using Night;
using UnityEngine;

namespace DefaultNamespace.Spells
{
    public class LightningStrikeSpell : SpellBattleInstance
    {
        protected override void Init()
        {
            Unit closestUnit = GetClosestUnitsInCastArea(CastTarget.ToVector2XZ()).FirstOrDefault();
            if (closestUnit != null)
            {
                closestUnit.DealDamage(Damage, null);
            }
            
            Deactivate();
        }
    }
}