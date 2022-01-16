using UnityEngine;

public class Ally : Character {
    public GameObject Race;
    public GameObject Class;
    public GameObject Weapon;

    public override void TakeTurn(int position) {
        base.TakeTurn(position);
        Debug.Log("ALLY TURN");
        
        Attack();
    }

    private void Attack() {
        Character enemy = CombatManager.Instance.GetFrontEnemy();
        Debug.Log("ATTACK: Front enemy " + enemy, this);
        enemy.TakeDamage(Power);
    }
}

