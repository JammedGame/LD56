namespace Night.Town 
{
    public class Wall : TownBuilding
    {
        protected override int[] Costs => new[] { 1, 2, 3 };

        protected override void UpdateState()
        {
            UserState.Instance.WallState.level = Level;
            UserState.Instance.WallState.currentHealthNormalized = 1f;
        }
    }
}