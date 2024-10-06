using UnityEngine;

namespace Night.Town 
{
    public class FrostSchool : TownBuilding
    {
        protected override int[] Costs => new[] { 2, 4, 6 };
    }
}