using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Data.Abilities {
    public abstract class Ability : ScriptableObject {
        public string Name;
        public string Description;
        
        public AbilityType Type;
        
        protected Character user;
        
        [SerializeField]
        private TargetStrategy targetStrategy;
        [SerializeField]
        private FilterStrategy filterStrategy;
        [SerializeField]
        private TriggerStrategy triggerStrategy;

        public virtual void Activate(Character user) {
            this.user = user;

            //  Determine if the action's trigger is fulfilled
            if (triggerStrategy != null && !triggerStrategy.Check(user)) {
                Debug.Log($"Trigger {triggerStrategy} not fulfilled for ability: {this}");
                return;
            }
            
            //  Get all targets based on the target strategy
            List<Character> targets = new List<Character>();
            if (targetStrategy != null) {
                targets = targetStrategy.Target(user);
            }
            else {
                Debug.LogWarning("No TargetStrategy on ability: " + this);
            }
            
            //  Filter targets if we have a filter strategy
            if (filterStrategy != null) {
                List<Character> filteredTargets = new List<Character>();
                foreach (var target in targets)
                {
                    if (filterStrategy.Filter(target)) {
                        filteredTargets.Add(target);
                    }
                }

                targets = filteredTargets;
            }
            
            //  Finally, use the ability on the (filtered) targets
            Use(targets);
        }
        
        public virtual void Deactivate(Character user) {}

        protected abstract void Use(IEnumerable<Character> targets);

        public override string ToString() {
            return $"{Name}: {Description}";
        }
    }

    public enum AbilityType {
        Active,
        Reactive,
        Passive
    }
}
