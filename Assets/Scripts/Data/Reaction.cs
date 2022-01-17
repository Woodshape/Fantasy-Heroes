using UnityEngine;

namespace Data {
    public interface IReaction {
        public bool BeforeAttack();
        public void EnableTrigger(Character trigger);
        public void DisableTrigger();
    }
}
