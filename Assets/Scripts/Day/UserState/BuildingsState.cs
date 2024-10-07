using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;

namespace Night
{
    public class BuildingsState
    {
        private readonly List<BuildingState> buildings;

        public IEnumerable<BuildingState> Buildings => buildings;

        public BuildingsState(List<BuildingState> buildings)
        {
            this.buildings = buildings;
        }

        public void AddOrUpdateBuilding(int slot, Action<BuildingState> modification)
        {
            var building = GetBuilding(slot);
            if (building == null)
            {
                building = new BuildingState(slot, null, 0);
                buildings.Add(building);
            }

            modification(building);
        }

        public BuildingState GetBuilding(int slot)
        {
            return buildings.Find(x => x.slot == slot);
        }

        public IEnumerable<UserEquippedSpell> GetSpells()
        {
            var buildingSpells = GameSettings.Instance.BuildingSpells
                .Where(x => buildings.Exists(y => y.type == x.building.GetType() && y.level >= x.minLevel));
            return buildingSpells.Select(x => new UserEquippedSpell(
                x.spell,
                buildings.Find(y => y.type == x.building.GetType()).level));
        }
    }

    public class BuildingState
    {
        public readonly int slot;
        public Type type;
        public int level;

        public BuildingState(int slot, Type type, int level)
        {
            this.slot = slot;
            this.type = type;
            this.level = level;
        }
    }
}