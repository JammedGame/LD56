using UnityEngine;

namespace Night.Town
{
	public class TownBuilding : MonoBehaviour
	{
		public int Level;
		protected int[] costs;
		public CurrencyManager currencyManager;

		public void Upgrade()
		{
			Level++;
			Debug.Log($"{gameObject.name} upgraded to level="+Level+"!");
		}

		private void OnMouseDown()
        {
			if (Level < costs.Length) {
				int cost = GetCurrentCost();
            	if(UserState.Gold.TrySpend(cost)) {
                	Upgrade();
					currencyManager.UpdateCurrencyUI(UserState.Gold.Currency);
            	} else{
                	Debug.Log("No money");
            	}
			} else {
				Debug.Log("Building is already at max level.");
			}
			
        }

		private int GetCurrentCost()
		{
			return costs[Level];
		}
	}
}