using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using Data.Abilities;
using UnityEngine;

public abstract class TargetStrategy : ScriptableObject {
    public abstract List<Character> Target(Character user);
}
