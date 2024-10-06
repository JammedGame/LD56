namespace Night
{
    public class WallState
    {
        public int level;
        public float currentHealthNormalized;

        public WallState(float startingHealthNormalized)
        {
            currentHealthNormalized = startingHealthNormalized;
        }
    }
}