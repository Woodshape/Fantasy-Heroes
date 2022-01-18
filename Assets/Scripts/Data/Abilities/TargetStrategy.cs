using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using Data.Abilities;
using UnityEngine;

public abstract class TargetStrategy : ScriptableObject {
    public abstract void Target(Character user, FilterStrategy filter, Action<IEnumerable<Character>> finished);
}
