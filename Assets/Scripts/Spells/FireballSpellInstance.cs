

using UnityEngine;

namespace DefaultNamespace.Spells
{
    public class FireballSpellInstance : SpellBattleInstance
    {
        private const float DamagePerLevel = 5;
        private const float MoveSpeedPerLevel = 5;
        
        private float damage;
        private float moveSpeed;
        
        protected override void Init()
        {
            damage = DamagePerLevel * SpellLevel;
            moveSpeed = MoveSpeedPerLevel * SpellLevel;
        }

        public override void Tick()
        {
            if (Vector3.Distance(transform.position, CastTarget) < 0.1f)
            {
                DealDamage();
                Deactivate();
                return;
            }
            
            transform.position = Vector3.MoveTowards(transform.position, CastTarget, moveSpeed * Time.deltaTime);
        }

        private void DealDamage()
        {
            Debug.Log($"Deal damage {damage}");
        }
    }
}