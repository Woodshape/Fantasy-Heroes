using System.Collections.Generic;
using UnityEngine;

namespace Data.Abilities {
    [CreateAssetMenu(fileName = "OrFilter", menuName = "Filter/OrFilter")]
    public class OrFilter : FilterStrategy {
        public List<FilterStrategy> filter;
        
        public override bool Filter(Character character) {
            foreach (var filterStrategy in filter) {
                if (filterStrategy.Filter(character)) {
                    return true;
                }
            }

            return false;
        }
    }
}
