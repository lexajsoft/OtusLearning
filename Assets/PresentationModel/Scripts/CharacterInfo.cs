using System;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;

namespace Lessons.Architecture.PM
{
    public sealed class CharacterInfo
    {
        public event Action<CharacterStat> OnStatAdded;
        public event Action<CharacterStat> OnStatRemoved;
    
        private readonly HashSet<CharacterStat> stats = new();

        public void AddStat(CharacterStat stat)
        {
            if (stats.Add(stat))
            {
                OnStatAdded?.Invoke(stat);
            }
        }

        public void RemoveStat(CharacterStat stat)
        {
            if (stats.Remove(stat))
            {
                OnStatRemoved?.Invoke(stat);
            }
        }

        public CharacterStat GetStat(string name)
        {
            foreach (var stat in stats)
            {
                if (stat.Name.Value == name)
                {
                    return stat;
                }
            }

            throw new Exception($"Stat {name} is not found!");
        }

        public CharacterStat[] GetStats()
        {
            return this.stats.ToArray();
        }
    }
}