using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Night.Town;

namespace Night
{
    public class UserState
    {
        public readonly Wallet Gold = new(100);

        public readonly WallState WallState = new(0, 1f);

        public readonly BuildingsState BuildingsState = new(new List<BuildingState>
        {
            new(1, typeof(Castle), 0),
            new(2, typeof(EarthSchool), 0),
            new(3, typeof(FireSchool), 0),
            new(4, typeof(FrostSchool), 0),
            new(5, typeof(StormSchool), 0)
        });

        public readonly ArmyState ArmyState = new(5);

        public int DayCount { get; private set; }

        public UserBattleData GetUserBattleData()
        {
            UserBattleData battleData = new();

            // TODO: read army state from town
            ArmyState.AddUnit(new UnitState(ArmyState.Units.Count(), new UserUnitInfo(GameSettings.Instance.Hedgehog, 0)));

            // SPELLS
            battleData.EquippedSpells.AddRange(BuildingsState.GetSpells());

            // ARMY
            battleData.UserUnits.AddRange(ArmyState.GetLivingUnits().Select(x => x.info));

            // WALL
            battleData.WallState = WallState;

            return battleData;
        }

        public void ApplyBattleWinResults(NightBattleContext battleContext)
        {
            DayCount++;

            // wall
            // stuff
        }

        // singleton shit
        private static UserState instance;
        public static UserState Instance => instance ??= new UserState();

        public static void Reset()
        {
            instance = new UserState();
        }
    }
}