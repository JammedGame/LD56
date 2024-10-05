using UnityEngine;

namespace Night.Town 
{
    public class FireSchool : TownBuilding
    {
        private void Awake()
        {
            costs = new int[] { 1, 5, 10 };
        }
    }
}