namespace Night.Town 
{
    public class Wall : TownBuilding
    {
        private void Awake()
        {
            costs = new int[] { 1, 2, 3 };
        }
    }
}