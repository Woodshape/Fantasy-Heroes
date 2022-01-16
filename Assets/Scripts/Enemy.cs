using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public override void TakeTurn(int position) {
        base.TakeTurn(position);
        Debug.Log("ENEMY TURN");
        
        Attack();
    }

    private void Attack() {
        Character ally = CombatManager.Instance.GetFrontAlly();
        Debug.Log("ATTACK: Front ally " + ally, this);
        ally.TakeDamage(Power);
    }
}
