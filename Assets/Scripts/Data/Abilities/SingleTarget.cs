using System;
using System.Collections.Generic;
using Data.Enemies;
using UnityEngine;

namespace Data {
    [CreateAssetMenu(fileName = "SingleTarget", menuName = "Target/SingleTarget")]
    public class SingleTarget : TargetStrategy {
        public override void Target(Character user, Action<IEnumerable<Character>> finished) {
            List<Character> targets = new List<Character>();
            switch (user) {
                case Ally _:
                    targets.Add(CombatManager.Instance.GetFrontEnemy());
                    break;
                case Enemy _:
                    targets.Add(CombatManager.Instance.GetFrontAlly());
                    break;
            }
            
            finished(targets);
        }
    }
}
