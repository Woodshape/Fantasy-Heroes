using System.Collections.Generic;
using UnityEngine;

namespace Data.Abilities {
    [CreateAssetMenu(fileName = "SelfTarget", menuName = "Target/SelfTarget")]
    public class SelfTarget : TargetStrategy {
        public override List<Character> Target(Character user) {
            List<Character> targets = new List<Character>();
            
            targets.Add(user);

            return targets;
        }
    }
}
