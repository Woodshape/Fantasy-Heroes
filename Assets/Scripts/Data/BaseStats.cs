using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data {
    public class BaseStats : MonoBehaviour {
        private readonly Dictionary<Stat, int> stats = new Dictionary<Stat, int>();
        private readonly List<Dictionary<Stat, int>> modifiers = new List<Dictionary<Stat, int>>();

        public void Setup(int power, int speed, int health) {
            stats.Add(Stat.Power, power);
            stats.Add(Stat.Speed, speed);
            stats.Add(Stat.Health, health);
        }

        public int GetStat(Stat stat) {
            int baseValue = GetBaseStatValue(stat);
            int modifiers = GetModifiers(stat);
            // float multipliers = GetMultipliers(stat);
            
            Debug.Log("GET Stat: " + stat, this);
            Debug.Log("Base: " + baseValue, this);
            Debug.Log("Mod: " + modifiers, this);

            int value = (baseValue + modifiers);
            
            Debug.Log("VALUE: " + value, this);

            return value;
        }

        public void AddModifier(Stat stat, int value) {
            Debug.Log($"Adding modifier to stat {stat}: {value}", this);
            modifiers.Add(new Dictionary<Stat, int>() {
                {stat, value}
            });
        }

        private int GetBaseStatValue(Stat stat) {
            return stats[stat];
        }
        
        private int GetModifiers(Stat stat) {
            int mod = 0;
            
            foreach (var modifier in modifiers) {
                if (modifier.ContainsKey(stat)) {
                    mod += modifier[stat];
                }
            }
            
            return mod;
        }
    }

    [Serializable]
    public class BaseStat {
        public Stat Stat;
        public int Value;
    }
}
