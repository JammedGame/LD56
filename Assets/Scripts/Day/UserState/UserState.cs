using System.Linq;
using DefaultNamespace;
using DefaultNamespace.Spells;

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

        public UserBattleData GetUserBattleData()
        {
            UserBattleData battleData = new();
            
            // SPELLS
            // TODO: read building state instead 
            foreach (SpellBookItem spell in GameSettings.Instance.Spells)
            {
                UserEquippedSpell userEquippedSpell = new UserEquippedSpell(spell, 1);
                battleData.EquippedSpells.Add(userEquippedSpell);
            }
		
            // ARMY
            battleData.UserUnits.AddRange(ArmyState.GetLivingUnits().Select(x => x.info));

            // TEMP HACK
            if (battleData.UserUnits.Count == 0)
            {
                battleData.UserUnits.Add(new UserUnitInfo(GameSettings.Instance.TestSpawn, 0));
            }

            // WAL
            // todo: read building state
            WallState.level = 0;
            WallState.currentHealthNormalized = 1f;
            
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