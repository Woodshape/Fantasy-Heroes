using System.Collections.Generic;
using Data.Buffs;
using UnityEngine;

namespace Data.Abilities {
    [CreateAssetMenu(fileName = "BuffAbility", menuName = "Abilities/BuffAbility")]
    public class BuffAbility : Ability {
        public Buff Buff;
        
        protected override void Use(IEnumerable<Character> targets) {
            foreach (var target in targets) {
                Buff.Apply(target);
            }
        }
    }
}
