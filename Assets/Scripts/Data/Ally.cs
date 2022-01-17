using Data.Classes;
using Data.Races;
using UnityEngine;

namespace Data {
    public class Ally : Character {
        public Race Race;
        public Class Class;
        public Weapon Weapon;

        public override void TakeTurn(int position) {
            base.TakeTurn(position);
            Debug.Log("ALLY TURN", this);
        
            CombatManager.Instance.PerformAttack(this);
        }
    
        public void SetRace(Race race) {
            if (Race != null) {
                Race.DisablePassive();
            }
        
            Race = race;

            if (Race != null) {
                Race.EnablePassive();
            }
        }
    
        public void SetClass(Class @class) {
            Class = @class;
            Reaction = @class;
        }
    }
}


