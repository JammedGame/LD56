using UnityEngine;

namespace DefaultNamespace.Spells
{
    [CreateAssetMenu]
    public class SpellShopItem : ScriptableObject
    {
        [SerializeField] private string id;
        [SerializeField] private string displayName;
        [SerializeField] private Sprite icon;
        
        /// <summary>
        /// The prefab that is instantiated when the spell is cast
        /// </summary>
        [SerializeField] private SpellInstance spellInstancePrefab;
        
        public string Id => id;
        public string DisplayName => displayName;
        public Sprite Icon => icon;
        public SpellInstance SpellPrefab => spellInstancePrefab;
    }
}