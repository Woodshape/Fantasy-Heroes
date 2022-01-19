using System.Collections.Generic;
using UnityEngine;

namespace Data.Abilities {
    [CreateAssetMenu(fileName = "AndFilter", menuName = "Filter/AndFilter")]
    public class AndFilter : FilterStrategy {
        public List<FilterStrategy> filter;
        
        public override bool Filter(Character character) {
            foreach (var filterStrategy in filter) {
                if (!filterStrategy.Filter(character)) {
                    return false;
                }
            }

            return true;
        }
    }
}
