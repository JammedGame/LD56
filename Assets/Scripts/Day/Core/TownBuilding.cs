using System;
using UnityEngine;

namespace Night.Town
{
    public abstract class TownBuilding : MonoBehaviour
    {
        public event Action<TownBuilding> SelectBuilding;

        private int level;
        private bool isSelected;

        protected abstract int[] Costs { get; }

        private int MaxLevel => Costs.Length;

        private int CurrentCost => Costs[level];

        private void OnMouseDown()
        {
            SelectBuilding?.Invoke(this);
        }

        public void ToggleSelected(bool selected)
        {
            isSelected = selected;
        }

        public void TryUpgrade()
        {
            if (level >= MaxLevel)
            {
                Debug.Log("Building is already at max level.");
                return;
            }

            int cost = CurrentCost;
            if (!UserState.Gold.TrySpend(cost))
            {
                Debug.Log("No money");
                return;
            }

            Upgrade();
            CurrencyManager.Instance.UpdateCurrencyUI(UserState.Gold.Currency);
        }

        private void Upgrade()
        {
            level++;
            Debug.Log($"{gameObject.name} upgraded to level=" + level + "!");
        }
    }
}