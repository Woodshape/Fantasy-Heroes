
using UnityEngine;

namespace Data.Races {
    public class Human : Race {
        public override void Setup() {
            base.Setup();

            Name = "Human";
        }

        public override void EnablePassive() {
            Debug.Log("I AM HUMAN!!!", this);

            character.Power += 1;
            character.Speed += 1;
            character.Health += 1;
        }

        public override void DisablePassive() {
            Debug.Log("THE HUMANITIES...", this);

            character.Power -= 1;
            character.Speed -= 1;
            character.Health -= 1;
        }
    }
}

