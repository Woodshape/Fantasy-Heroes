using System.Collections.Generic;
using UnityEngine;

namespace Data.Abilities {
    public abstract class Ability : ScriptableObject {
        public AbilityType Type;
        
        protected Character user;
        
        [SerializeField]
        private TargetStrategy targetStrategy;
        [SerializeField]
        private TriggerStrategy triggerStrategy;

        public void TryUse(Character user) {
            this.user = user;

            if (triggerStrategy != null && !triggerStrategy.Check(user)) {
                Debug.Log($"Trigger {triggerStrategy} not fulfilled for ability: {this}");
                return;
            }
            
            if (targetStrategy != null) {
                targetStrategy.Target(user, Use);
            }
        }

        protected abstract void Use(IEnumerable<Character> targets);
    }

    public enum AbilityType {
        Active,
        Reactive,
        Passive
    }
}
