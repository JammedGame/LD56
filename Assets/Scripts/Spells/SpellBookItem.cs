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
        
        /// <summary>
        /// The prefab that is instantiated when the spell is cast in-game
        /// </summary>
        [SerializeField] private SpellBattleInstance spellRuntimePrefab;
        
        public string Id => id;
        public string DisplayName => displayName;
        public Sprite Icon => icon;
        public SpellBattleInstance SpellBattlePrefab => spellRuntimePrefab;
    }
}