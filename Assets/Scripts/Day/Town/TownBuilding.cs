using UnityEngine;

namespace Night.Town
{
	public class TownBuilding : MonoBehaviour
	{
		public int Level;
		protected int[] costs;

		public void Upgrade()
		{
			if (Level < costs.Length - 1) {
				Level++;
				Debug.Log($"{gameObject.name} upgraded!");
			} else {
				Debug.Log("Building is already at max level.");
			}
		}

		private void OnMouseDown()
        {
			int cost = GetCurrentCost();
            if(UserState.Gold.TrySpend(cost)) {
                Upgrade();
            } else{
                Debug.Log("No money");
            }
        }

		private int GetCurrentCost()
		{
			return costs[Level];
		}
	}
}