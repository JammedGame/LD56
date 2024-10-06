using DefaultNamespace.Spells;
using UnityEngine;

namespace Night
{
    public class UserEquippedSpell
    {
        public readonly SpellBookItem Blueprint;
        public readonly int Level;

        private float cooldownLeft;

        public Vector2 CastArea => Blueprint.SpellBattlePrefab.CalculateCastArea(Level, Blueprint.AreaOfEffect);
        public float Damage => Blueprint.SpellBattlePrefab.CalculateDamage(Level, Blueprint.Damage);
        public float MoveSpeed => Blueprint.SpellBattlePrefab.CalculateMoveSpeed(Level, Blueprint.MoveSpeed);
        public float EffectDuration => Blueprint.SpellBattlePrefab.CalculateEffectDuration(Level, Blueprint.EffectDuration);

        public float CooldownLeft
        {
            get => cooldownLeft;
            set
            {
                if (value <= 0f)
                {
                    cooldownLeft = 0f;
                    return;
                }

                cooldownLeft = value;
            }
        }

        public bool IsOnCooldown => CooldownLeft > 0f;

        public UserEquippedSpell(SpellBookItem blueprint, int level)
        {
            Level = level;
            Blueprint = blueprint;
        }

        public void StartCooldown()
        {
            // No cooldown for rapid fire spells
            if (Blueprint.IsRapidFire)
            {
                cooldownLeft = Blueprint.RapidFireCooldown;
            }
            else
            {
                cooldownLeft = Blueprint.CastCooldown;
            }
        }
    }
}