using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

public abstract class TargetStrategy : ScriptableObject {
    public abstract void Target(Character user, Action<IEnumerable<Character>> finished);
}
