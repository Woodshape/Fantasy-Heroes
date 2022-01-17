using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Races {
    public abstract class Race : MonoBehaviour {
        public string Name;

        protected Ally character;

        public virtual void Setup() {
            character = GetComponent<Ally>();
        }

        public abstract void EnablePassive();
        public abstract void DisablePassive();
    }
}

