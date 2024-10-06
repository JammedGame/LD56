namespace Night.Town 
{
    public class CityHall : TownBuilding
    {
        protected override int[] Costs => new[] { 1, 10, 20 };
    }
}