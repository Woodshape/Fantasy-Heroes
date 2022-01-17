using UnityEngine;

namespace Data.Enemies {
    public class Skeleton : Enemy {
        public override void Setup() {
            base.Setup();
            
            Debug.Log("I AM A SKELLIE-TON!!!", this);

            Reaction = new SkeletonReaction(this);
        }
        
        private class SkeletonReaction : IReaction {
            private readonly Enemy enemy;
            
            public SkeletonReaction(Enemy enemy) {
                this.enemy = enemy;
            }

            public string GetDescription() {
                return "Deals 1 damage to attacker on death.";
            }
            
            public TriggerType GetTriggerType() {
                return TriggerType.OnDeath;
            }
            
            public void EnableTrigger(Character trigger) {
                Debug.Log(GetDescription(), enemy);

                trigger.TakeDamage(enemy, 1, true);
            }
            
            public void DisableTrigger() { }
        }
    }
}
