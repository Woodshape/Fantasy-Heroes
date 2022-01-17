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
            
            public bool BeforeAttack() {
                return true;
            }
            
            public void EnableTrigger(Character trigger) {
                Debug.Log("SKELLIES ARE HARD");

                enemy.Armor += 1;
            }
            
            public void DisableTrigger() {
                Debug.Log("SKELLIES ARE NO LONGER HARD...");

                enemy.Armor -= 1;
            }
        }
    }
}
