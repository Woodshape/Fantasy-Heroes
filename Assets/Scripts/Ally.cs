using UnityEngine;

public class Ally : Character {
    public Race Race;
    public Class Class;
    public Weapon Weapon;

    public override void TakeTurn(int position) {
        base.TakeTurn(position);
        Debug.Log("ALLY TURN", this);
        
        Attack();
    }
    
    public void SetRace(Race race) {
        if (Race != null) {
            Race.RemovePassive();
        }
        
        Race = race;

        if (Race != null) {
            Race.ApplyPassive();
        }
    }

    private void Attack() {
        Character enemy = CombatManager.Instance.GetFrontEnemy();
        Debug.Log("ATTACK: Front enemy " + enemy, this);
        enemy.TakeDamage(Power);
    }
}

