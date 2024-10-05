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
	}
}