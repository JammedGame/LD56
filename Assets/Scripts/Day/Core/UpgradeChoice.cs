using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Night.Town
{
    public class UpgradeChoice : MonoBehaviour
    {
        [SerializeField] private Button button;

        public void Initialize(TownBuilding buildingPrefab, UnityAction onClick)
        {
            var building = Instantiate(buildingPrefab, transform);
            building.GetComponent<Image>().raycastTarget = false;
            var rectTransform = (RectTransform)building.transform;
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
            button.SetListener(onClick);
        }
    }
}