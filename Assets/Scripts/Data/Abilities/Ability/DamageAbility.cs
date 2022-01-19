using System;
using System.Collections.Generic;
using Data.Abilities;
using UnityEngine;

namespace Data {
    [CreateAssetMenu(fileName = "DamageAbility", menuName = "Abilities/DamageAbility")]
    public class DamageAbility : Ability {
        public int Damage;
        public bool IgnoreArmor;

        protected override void Use(IEnumerable<Character> targets) {
            Debug.Log(user + " using " + this);
            foreach (var target in targets) {
                target.TakeDamage(user, Damage, IgnoreArmor);
            }
        }
    }
}
