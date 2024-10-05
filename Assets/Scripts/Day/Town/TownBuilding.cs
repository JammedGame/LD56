using UnityEngine;

namespace Night.Town
{
	public class TownBuilding : MonoBehaviour
	{
		public int Level;

		public void Upgrade()
		{
			Level++;
		}

		private void OnMouseDown()
        {
            if(UserState.Gold.TrySpend(1)) {
                Upgrade();
				Debug.Log($"{gameObject.name} upgraded!");
            } else{
                Debug.Log("No money");
            }
        }
	}
}