namespace Night
{
    public class WallState
    {
        public int level;
        public float currentHealthNormalized;

        public WallState(int startingLevel, float startingHealthNormalized)
        {
            level = startingLevel;
            currentHealthNormalized = startingHealthNormalized;
        }
    }
}