using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace.Spells
{
    /// <summary>
    /// Basic data around one spell available in the game.
    /// </summary>
    [CreateAssetMenu]
    public class SpellBookItem : ScriptableObject
    {
        [SerializeField] private string id;
        [SerializeField] private string displayName;
        [SerializeField] private Sprite icon;
        [SerializeField] private bool isRapidFire;
        [SerializeField] private float rapidFireCooldown;
        [SerializeField] private float castCooldown;
        [SerializeField] private Vector2 areaOfEffect;
        [SerializeField] private float damage;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float effectDuration;
        
        
        /// <summary>
        /// The prefab that is instantiated when the spell is cast in-game
        /// </summary>
        [SerializeField] private SpellBattleInstance spellRuntimePrefab;
        
        public string Id => id;
        public string DisplayName => displayName;
        public Sprite Icon => icon;
        public SpellBattleInstance SpellBattlePrefab => spellRuntimePrefab;
        public bool IsRapidFire => isRapidFire;
        public float RapidFireCooldown => rapidFireCooldown;
        public float CastCooldown => castCooldown;
        public Vector2 AreaOfEffect => areaOfEffect;
        public float Damage => damage;
        public float MoveSpeed => moveSpeed;
        public float EffectDuration => effectDuration;
    }
}