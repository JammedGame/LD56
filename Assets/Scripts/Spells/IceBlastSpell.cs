using System.Collections;
using System.Collections.Generic;
using Night;
using UnityEngine;

namespace DefaultNamespace.Spells
{
    public class IceBlastSpell : SpellBattleInstance
    {
        [SerializeField] private GameObject explosionVisualEffectPrefab;

        public override void Tick()
        {
            if (Vector3.Distance(CastTarget, transform.position) < 0.1f)
            {
                transform.position = CastTarget;
                Instantiate(explosionVisualEffectPrefab, CastTarget, explosionVisualEffectPrefab.transform.rotation);
                
                List<Unit> units = GetClosestUnitsInCastArea(CastTarget.ToVector2XZ());
                foreach (Unit unit in units)
                {
                    unit.DealDamage(Damage, null);
                    unit.AddModifier(new UnitModifier(3, 0.3f, colorTint: new Color(0.5f, 0.5f, 1f, 1)));
                }
                
                Deactivate();
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, CastTarget, Time.deltaTime * MoveSpeed);
        }
    }
}