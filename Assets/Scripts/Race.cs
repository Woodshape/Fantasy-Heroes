using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Race : MonoBehaviour {
    public string Name;

    public abstract void ApplyPassive();
    public abstract void RemovePassive();
}
