using UnityEngine;

namespace Data {
    public interface IReaction {
        public string GetDescription();
        public TriggerType GetTriggerType();
        public void EnableTrigger(Character trigger);
        public void DisableTrigger();
    }
    
    public enum TriggerType {
        BeforeAttack,
        AfterAttack,
        OnHurt,
        OnDeath,
        RoundStart,
        RoundEnd
    }
}
