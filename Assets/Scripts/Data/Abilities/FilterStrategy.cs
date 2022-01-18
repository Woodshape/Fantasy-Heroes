using UnityEngine;

namespace Data.Abilities {
    public abstract class FilterStrategy : ScriptableObject {
        public abstract bool Filter(Character character);
    }
}
