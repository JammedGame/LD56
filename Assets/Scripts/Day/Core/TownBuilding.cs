using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Night.Town
{
    public abstract class TownBuilding : MonoBehaviour, IPointerDownHandler
    {
        public event Action<TownBuilding> SelectBuilding;

        public int Level { get; private set; }

        protected abstract int[] Costs { get; }

        private int MaxLevel => Costs.Length;

        [SerializeField] public Sprite BuildingSprite;
        [SerializeField] public Sprite BuildingSelectedSprite;

        public int CurrentCost => Level < Costs.Length ? Costs[Level] : int.MaxValue;

        private Image image;

        private void Awake()
        {
            image = GetComponent<Image>();

            if (image == null)
            {
                Debug.LogError("No Image component found on this GameObject. Please add one.");
            }
        }

        public void SetSprite(Sprite sprite)
        {
            image.sprite = sprite;
        }

        public Sprite GetSprite()
        {
            return image.sprite;
        }

        public void ToggleSelected(bool selected)
        {
            if (selected)
            {
                SetSprite(BuildingSelectedSprite);
            } else
            {
                SetSprite(BuildingSprite);
            }
        }

        public void TryUpgrade()
        {
            if (Level >= MaxLevel)
            {
                Debug.Log("Building is already at max level.");
                return;
            }

            int cost = CurrentCost;
            if (!UserState.Instance.Gold.TrySpend(cost))
            {
                Debug.Log("No money");
                return;
            }

            Upgrade();
        }

        private void Upgrade()
        {
            Level++;
            Debug.Log($"{gameObject.name} upgraded to level=" + Level + "!");
            SelectThisBuilding();
        }

        private void SelectThisBuilding()
        {
            SelectBuilding?.Invoke(this);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            SelectThisBuilding();
        }
    }
}