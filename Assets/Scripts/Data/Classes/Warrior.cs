using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Classes {
    public class Warrior : Class
    {
        public override void Setup() {
            base.Setup();

            Name = "Warrior";
            Description = "I AM A MIGHTY WARRIOR!!!";
            
            Debug.Log($"{Name}: {Description}", this);
        }
        
        public override string GetDescription() {
            return "Deals 1 damage to enemy on being hurt.";
        }

        public override TriggerType GetTriggerType() {
            return TriggerType.OnHurt;
        }

        public override void EnableTrigger(Character trigger) {
            Debug.Log(GetDescription(), this);
        
            trigger.TakeDamage(character,1, true);
        }
        
        public override void DisableTrigger() { }
    }
}