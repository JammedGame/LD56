using System;
using System.Collections.Generic;
using System.Linq;

namespace Night
{
    public class ArmyState
    {
        private readonly int maxUnits;
        private readonly List<UnitState> units = new();

        public IEnumerable<UnitState> Units => units;

        public ArmyState(int maxUnits)
        {
            this.maxUnits = maxUnits;
        }

        public void AddUnit(UnitState newUnit)
        {
            if (units.Count >= maxUnits)
            {
                throw new InvalidOperationException("Max units added");
            }

            if (units.Exists(x => x.slot == newUnit.slot))
            {
                throw new InvalidOperationException($"Unit already exists at slot {newUnit.slot}");
            }

            units.Add(newUnit);
        }

        public void ModifyUnit(int slot, Action<UnitState> modification)
        {
            if (!units.Exists(x => x.slot == slot))
            {
                throw new InvalidOperationException($"Unit not found at slot {slot}");
            }

            var spell = GetUnit(slot);
            modification(spell);
        }

        public UnitState GetUnit(int slot)
        {
            return units.Find(x => x.slot == slot);
        }

        public IEnumerable<UnitState> GetLivingUnits()
        {
            return units.Where(x => !x.isDead);
        }
    }

    public class UnitState
    {
        public readonly int slot;
        public readonly UserUnitInfo info;
        public bool isDead;

        public UnitState(int slot, UserUnitInfo info)
        {
            this.slot = slot;
            this.info = info;
        }
    }
}