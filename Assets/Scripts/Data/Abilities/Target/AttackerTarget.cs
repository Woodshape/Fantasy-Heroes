using System.Collections.Generic;
using UnityEngine;

namespace Data.Abilities {
    [CreateAssetMenu(fileName = "AttackerTarget", menuName = "Target/AttackerTarget")]
    public class AttackerTarget : TargetStrategy {
        public override List<Character> Target(Character user) {
            List<Character> targets = new List<Character>();

            targets.Add(user.AttackInformation.Attacker);
            
            return targets;
        }
    }
}
