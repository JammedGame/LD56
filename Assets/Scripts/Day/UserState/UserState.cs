namespace Night
{
    public class UserState
    {
        public readonly Wallet Gold = new(100);

        public readonly TownHallState TownHallState = new();
        public readonly WallState WallState = new(1f);
        public readonly SpellBookState SpellBookState = new();
        public readonly ArmyState ArmyState = new(5);
        public int DayCount { get; private set; }

        // singleton shit
        private static UserState instance;
        public static UserState Instance => instance ??= new UserState();

        public static void Reset()
        {
            instance = new UserState();
        }
    }
}