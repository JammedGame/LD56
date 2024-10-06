namespace Night.Town 
{
    public class Wall : TownBuilding
    {
        protected override int[] Costs => new[] { 1, 2, 3 };
    }
}