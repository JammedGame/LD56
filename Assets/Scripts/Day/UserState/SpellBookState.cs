using System;
using System.Collections.Generic;

namespace Night
{
    public class SpellBookState
    {
        private readonly List<UserEquippedSpell> spells = new();

        public IEnumerable<UserEquippedSpell> Spells => spells;

        public void AddSpell(UserEquippedSpell newSpell)
        {
            if (spells.Exists(x => x.Blueprint.Id == newSpell.Blueprint.Id))
            {
                throw new InvalidOperationException($"Spell already exists for ID {newSpell.Blueprint.Id}");
            }

            spells.Add(newSpell);
        }

        public void ModifySpell(string id, Action<UserEquippedSpell> modification)
        {
            if (!spells.Exists(x => x.Blueprint.Id == id))
            {
                throw new InvalidOperationException($"Spell not found for ID {id}");
            }

            var spell = GetSpell(id);
            modification(spell);
        }

        public UserEquippedSpell GetSpell(string id)
        {
            return spells.Find(x => x.Blueprint.Id == id);
        }
    }
}