
using UnityEngine;

namespace Data.Races {
    public class Human : Race {
        public override void Setup() {
            base.Setup();

            Name = "Human";
            Description = "I AM HUMAN!!!";

            Debug.Log($"{Name}: {Description}");
        }

        public override void EnablePassive() {
            Debug.Log($"Enabling {Name} passive", this);

            character.Power += 1;
            character.Speed += 1;
            character.Health += 1;
        }

        public override void DisablePassive() {
            Debug.Log($"Disabling {Name} passive", this);

            character.Power -= 1;
            character.Speed -= 1;
            character.Health -= 1;
        }
    }
}

