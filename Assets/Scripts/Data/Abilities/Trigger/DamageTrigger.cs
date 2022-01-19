using System;
using UnityEngine;

namespace Data.Abilities {
    [CreateAssetMenu(fileName = "DamageTrigger", menuName = "Trigger/DamageTrigger")]
    public class DamageTrigger : TriggerStrategy {
        public bool OnDeath;
        
        public override bool Check(Character user) {
            if (OnDeath && user.IsDead()) {
                return true;
            }
            if (!OnDeath && user.IsHurt()) {
                return true;
            }

            return false;
        }
    }
}
