using System;
using UnityEngine;

namespace Night.Town
{
    public abstract class TownBuilding : MonoBehaviour
    {
        public event Action<TownBuilding> SelectBuilding;

        private bool isSelected;

        public int Level { get; private set; }

        protected abstract int[] Costs { get; }

        private int MaxLevel => Costs.Length;

        public int CurrentCost => Level < Costs.Length ? Costs[Level] : int.MaxValue;

        private void OnMouseDown()
        {
            SelectThisBuilding();
        }

        public void ToggleSelected(bool selected)
        {
            isSelected = selected;
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
            CurrencyManager.Instance.UpdateCurrencyUI(UserState.Instance.Gold.Currency);
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
    }
}