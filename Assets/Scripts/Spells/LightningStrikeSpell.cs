

using System.Linq;
using FirstGearGames.SmoothCameraShaker;
using Night;
using UnityEngine;

namespace DefaultNamespace.Spells
{
    public class LightningStrikeSpell : SpellBattleInstance
    {
        [SerializeField] private GameObject visualEffectPrefab;
        [SerializeField] private ShakeData shakeData;
        [SerializeField] private AudioClip onCastAudio;

        protected override void Init()
        {
            Instantiate(visualEffectPrefab, CastTarget, visualEffectPrefab.transform.rotation);
            Unit closestUnit = GetClosestUnitsInCastArea(CastTarget.ToVector2XZ()).FirstOrDefault();
            if (closestUnit != null)
            {
                closestUnit.DealDamage(Damage, null);
                Color color = new Color(0.3f, 0.3f, 0.3f, 1);
                closestUnit.AddModifier(new UnitModifier(0.15f, colorTint: color));
            }
            
            CameraShakerHandler.Shake(shakeData);
            GlobalAudioSource.PlayOneShot(onCastAudio);
            Deactivate();
        }
    }
}