
using UnityEngine;

public class Human : Race {
    public override void ApplyPassive() {
        Debug.Log("I AM HUMAN!!!", this);
        
        Ally character = GetComponent<Ally>();
        character.Power += 1;
        character.Speed += 1;
        character.Health += 1;
    }
    
    public override void RemovePassive() {
        Debug.Log("THE HUMANITIES...", this);
    }
}

