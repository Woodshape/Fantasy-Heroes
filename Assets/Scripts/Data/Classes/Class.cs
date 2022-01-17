using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Data.Classes {
    public abstract class Class : MonoBehaviour, IReaction
    {
        public string Name;
        public string Description;

        protected Ally character;

        public virtual void Setup() {
            character = GetComponent<Ally>();
        }

        public abstract string GetDescription();
        public abstract TriggerType GetTriggerType();
        public abstract void EnableTrigger(Character trigger);
        public abstract void DisableTrigger();
    }
}

