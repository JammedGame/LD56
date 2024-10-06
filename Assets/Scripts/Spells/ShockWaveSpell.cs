using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Night;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace.Spells
{
    public class ShockWaveSpell : SpellBattleInstance
    {
        [SerializeField] private GameObject quakeVisualEffectPrefab;

        private float hitTime;

        protected override void Init()
        {
            hitTime = Time.time;
            transform.position = CastTarget;

            Instantiate(quakeVisualEffectPrefab, CastTarget, quakeVisualEffectPrefab.transform.rotation, transform);
            StartCoroutine(DoEffectOverTime());
        }

        private IEnumerator DoEffectOverTime()
        {
            while (Time.time - hitTime < SpellInfo.EffectDuration)
            {
                List<Unit> units = GetClosestUnitsInCastArea(CastTarget.ToVector2XZ());
                foreach (Unit unit in units)
                {
                    unit.DealDamage(Damage, null);
                    unit.AddModifier(new UnitModifier(1f, moveSpeedMod: 0.8f));
                }
                
                yield return new WaitForSeconds(0.2f);
            }
            
            // Extinguish effects slowly
            foreach (ParticleSystem system in GetComponentsInChildren<ParticleSystem>())
            {
                system.Stop();
            }
            
            Deactivate();
        }

        protected override void OnDeactivate()
        {
            StartCoroutine(DestroyAfterSeconds(2f));
        }
    }
}