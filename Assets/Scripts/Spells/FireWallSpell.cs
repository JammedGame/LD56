using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Night;
using UnityEngine;

namespace DefaultNamespace.Spells
{
    public class FireWallSpell : SpellBattleInstance
    {
        [SerializeField] private GameObject explosionVisualEffectPrefab;
        [SerializeField] private GameObject burningVisualEffectPrefab;

        private float hitTime;
        
        public override void Tick()
        {
            if (hitTime > 0f)
            {
                return;
            }
            
            if (Vector3.Distance(CastTarget, transform.position) < 0.1f)
            {
                hitTime = Time.time;
                transform.position = CastTarget;
                Instantiate(explosionVisualEffectPrefab, CastTarget, explosionVisualEffectPrefab.transform.rotation);

                float posZ = CastTarget.z - AreaOfEffect.y / 2f - 1f;
                for (; posZ <= CastTarget.z + AreaOfEffect.y / 2f + 1f; posZ += 1f)
                {
                    Instantiate(burningVisualEffectPrefab, new Vector3(CastTarget.x, CastTarget.y, posZ), burningVisualEffectPrefab.transform.rotation, transform);
                }

                StartCoroutine(DealDamageOverTime());
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, CastTarget, Time.deltaTime * MoveSpeed);
        }

        private IEnumerator DealDamageOverTime()
        {
            while (Time.time - hitTime < SpellInfo.EffectDuration)
            {
                List<Unit> units = GetClosestUnitsInCastArea(CastTarget.ToVector2XZ());
                foreach (Unit unit in units)
                {
                    unit.DealDamage(Damage, null);
                }
                
                yield return new WaitForSeconds(0.2f);
            }
            
            // Extinguish fire
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