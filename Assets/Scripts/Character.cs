using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    public string Name;
    
    public int Power;
    public int Health;
    public int Speed;

    public List<GameObject> Buffs;
    public List<GameObject> Conditions;

    private bool isDead;

    public virtual void TakeTurn(int position) {
        Debug.Log($"Taking turn at position {position}: {this}", this);
        Debug.Log("Front ally: " + CombatManager.Instance.GetFrontAlly(), this);
        Debug.Log("Front enemy: " + CombatManager.Instance.GetFrontEnemy(), this);
    }

    public void TakeDamage(int amount) {
        Debug.Log($"{Name} taking {amount} damage!");
        Health -= amount;
    }

    public void EvaluateHealth() {
        Debug.Log($"{Name} evaluating health: {Health}");
        
        if (Health <= 0) {
            isDead = true;
        }
    }

    private void Die() {
        Debug.Log($"{Name} dies!");
        // CombatManager.Instance.Remove
    }

    public bool IsDead() {
        return isDead;
    }

    public override string ToString() {
        return $"{Name} -> P:{Power} | S:{Speed} | H:{Health}";
    }
}
