using System;
using System.Collections.Generic;
using Data.Enemies;
using UnityEngine;

namespace Data.Abilities {
    [CreateAssetMenu(fileName = "PositionTarget", menuName = "Target/PositionTarget")]
    public class PositionTarget : TargetStrategy {
        public List<int> Positions;
        public bool OnlyFront;
        
        public override List<Character> Target(Character user) {
            List<Character> targets = new List<Character>();

            if (OnlyFront) {
                Ally ally = CombatManager.Instance.GetFrontAlly();
                if (ally != null) {
                    targets.Add(ally);
                }
                Enemy enemy = CombatManager.Instance.GetFrontEnemy();
                if (enemy != null) {
                    targets.Add(enemy);
                }
            }
            else {
                foreach (var position in Positions) {
                    Ally ally = CombatManager.Instance.GetAlly(position);
                    if (ally != null) {
                        targets.Add(ally);
                    }
                
                    Enemy enemy = CombatManager.Instance.GetEnemy(position);
                    if (enemy != null) {
                        targets.Add(enemy);
                    }
                }
            }

            return targets;
        }
    }
}
