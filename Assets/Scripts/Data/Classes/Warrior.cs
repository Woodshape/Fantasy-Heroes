using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Classes {
    public class Warrior : Class
    {
        public override void Setup() {
            base.Setup();

            Debug.Log("I AM A MIGHTY WARRIOR!!!", this);

            Name = "Warrior";
        }
    
        public override bool BeforeAttack() {
            return true;
        }

        public override void EnableTrigger(Character trigger) {
            Debug.Log("REVENGE", this);
        
            trigger.TakeDamage(1, true);
        }
        
        public override void DisableTrigger() { }
    }
}