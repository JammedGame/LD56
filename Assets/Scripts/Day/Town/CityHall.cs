namespace Night.Town 
{
    public class CityHall : TownBuilding
    {
        private void Awake()
        {
            costs = new int[] { 1, 10, 20 };
        }
    }
}