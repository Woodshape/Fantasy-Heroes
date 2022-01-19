using System.Collections.Generic;
using UnityEngine;

namespace Data.Abilities {
    [CreateAssetMenu(fileName = "AllTarget", menuName = "Target/AllTarget")]
    public class AllTarget : TargetStrategy {
        public override List<Character> Target(Character user) {
            List<Character> targets = new List<Character>();

            foreach (var ally in CombatManager.Instance.GetAllies()) {
                targets.Add(ally);
            }
            foreach (var enemy in CombatManager.Instance.GetEnemies()) {
                targets.Add(enemy);
            }
            
            return targets;
        }
    }
}
