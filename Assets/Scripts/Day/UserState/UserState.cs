using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using DefaultNamespace.Spells;
using Night.Town;

namespace Night
{
    public class UserState
    {
        public readonly Wallet Gold = new(100);

        public readonly WallState WallState = new(0, 1f);

        public readonly BuildingsState BuildingsState = new(new List<BuildingState>
        {
            new(1, typeof(Castle), 1),
            new(2, typeof(EarthSchool), 1),
            new(3, typeof(FireSchool), 1),
            new(4, typeof(FrostSchool), 1),
            new(5, typeof(StormSchool), 1)
        });

        public readonly SpellBookState SpellBookState = new();

        public readonly ArmyState ArmyState = new(5);

        public int DayCount { get; private set; }

        public UserBattleData GetUserBattleData()
        {
            UserBattleData battleData = new();

            // SPELLS
            battleData.EquippedSpells.AddRange(SpellBookState.EquippedSpells);

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

    public class SpellBookState
    {
        public readonly List<UserEquippedSpell> EquippedSpells = new();

        public int GetSpellLevel(SpellBookItem itemV)
        {
            foreach (UserEquippedSpell item in EquippedSpells)
            {
                if (item.Blueprint == itemV)
                {
                    return item.Level;
                }
            }

            return 0;
        }

        public void UpgradeSpell(SpellBookItem mySpell)
        {
            foreach (UserEquippedSpell item in EquippedSpells)
            {
                if (item.Blueprint == mySpell)
                {
                    item.Level++;
                    return;
                }
            }

            EquippedSpells.Add(new UserEquippedSpell(mySpell, 1));
        }
    }
}