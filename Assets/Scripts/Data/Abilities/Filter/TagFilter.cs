using UnityEngine;

namespace Data.Abilities {
    [CreateAssetMenu(fileName = "TagFilter", menuName = "Filter/TagFilter")]
    public class TagFilter : FilterStrategy {
        public string tag;
        
        public override bool Filter(Character character) {
            return character.gameObject.tag.Equals(tag);
        }
    }
}
