

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
            }
            
            CameraShakerHandler.Shake(shakeData);
            GlobalAudioSource.PlayOneShot(onCastAudio);
            Deactivate();
        }
    }
}