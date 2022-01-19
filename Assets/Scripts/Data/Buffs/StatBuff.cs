using System.Collections.Generic;
using UnityEngine;

namespace Data.Buffs {
    [CreateAssetMenu(fileName = "StatBuff", menuName = "Buffs/StatBuff")]
    public class StatBuff : Buff {
        public BaseStat[] Stats;
        
        public override void Apply(Character target) {
            foreach (var stat in Stats) {
                target.Stats.AddModifier(stat.Stat, stat.Value);
            }
        }
    }
}
