

using System.Linq;
using Night;
using UnityEngine;

namespace DefaultNamespace.Spells
{
    public class LightningStrikeSpell : SpellBattleInstance
    {
        [SerializeField] private GameObject visualEffectPrefab;
        
        protected override void Init()
        {
            Instantiate(visualEffectPrefab, CastTarget, visualEffectPrefab.transform.rotation);
            Unit closestUnit = GetClosestUnitsInCastArea(CastTarget.ToVector2XZ()).FirstOrDefault();
            if (closestUnit != null)
            {
                closestUnit.DealDamage(Damage, null);
            }
            
            Deactivate();
        }
    }
}