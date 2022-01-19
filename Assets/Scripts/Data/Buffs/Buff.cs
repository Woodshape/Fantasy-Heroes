using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Data.Buffs {
    public abstract class Buff : ScriptableObject {
        public abstract void Apply(Character target);
    }
}