using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Night.Town
{
    [RequireComponent(typeof(Image))]
    public abstract class TownBuilding : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] public int Slot;
        [SerializeField] private Sprite BuildingSprite;
        [SerializeField] private Sprite BuildingSelectedSprite;

        [field: SerializeField] public TownBuilding[] UpgradeChoices { get; private set; }

        public event Action<TownBuilding> SelectBuilding;
        public event Action<TownBuilding> OfferUpgradeChoices;

        public int Level { get; private set; }

        protected abstract int[] Costs { get; }

        private int MaxLevel => Costs.Length;

        public int CurrentCost => Level < Costs.Length ? Costs[Level] : int.MaxValue;

        private Image image;

        private void Awake()
        {
            image = GetComponent<Image>();
            UpdateState();
        }

        public void ToggleSelected(bool selected)
        {
            image.sprite = selected ? BuildingSelectedSprite : BuildingSprite;
        }

        public void TryUpgrade()
        {
            if (Level >= MaxLevel && UpgradeChoices.Length == 0)
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
            if (UpgradeChoices.Length > 0)
            {
                OfferUpgradeChoices?.Invoke(this);
                return;
            }

            Level++;
            Debug.Log($"{gameObject.name} upgraded to level=" + Level + "!");
            UpdateState();
            SelectThisBuilding();
        }

        protected virtual void UpdateState()
        {
            var type = GetType();
            UserState.Instance.BuildingsState.AddOrUpdateBuilding(Slot, state =>
            {
                state.type = type;
                state.level = Level;
            });
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