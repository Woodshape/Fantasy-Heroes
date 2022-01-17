using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Enemies {
    public class Enemy : Character
    {
        public override void TakeTurn(int position) {
            base.TakeTurn(position);
            Debug.Log("ENEMY TURN", this);
        
            CombatManager.Instance.PerformAttack(this);
        }
    }
}