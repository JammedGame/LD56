namespace Night.Town 
{
    public class Castle : TownBuilding
    {
        protected override int[] Costs => new[] { 1, 10, 20 };
    }
}