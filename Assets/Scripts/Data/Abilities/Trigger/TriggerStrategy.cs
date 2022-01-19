using UnityEngine;

namespace Data.Abilities {
    public abstract class TriggerStrategy : ScriptableObject {
        public abstract bool Check(Character user);
    }
}
