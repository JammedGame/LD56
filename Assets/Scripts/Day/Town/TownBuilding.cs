using UnityEngine;

namespace Night.Town
{
    public abstract class TownBuilding : MonoBehaviour
    {
        private int level;
        protected abstract int[] Costs { get; }

        private void Upgrade()
        {
            level++;
            Debug.Log($"{gameObject.name} upgraded to level=" + level + "!");
        }

        private void OnMouseDown()
        {
            if (level < Costs.Length)
            {
                int cost = GetCurrentCost();
                if (UserState.Gold.TrySpend(cost))
                {
                    Upgrade();
                    CurrencyManager.Instance.UpdateCurrencyUI(UserState.Gold.Currency);
                }
                else
                {
                    Debug.Log("No money");
                }
            }
            else
            {
                Debug.Log("Building is already at max level.");
            }
        }

        private int GetCurrentCost()
        {
            return Costs[level];
        }
    }
}