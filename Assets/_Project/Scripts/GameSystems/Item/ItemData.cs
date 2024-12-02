using UnityEngine;

namespace TheLastLand._Project.Scripts.GameSystems.Item
{
    [CreateAssetMenu(fileName = "New Item", menuName = "TheLastLand/ItemData", order = 0)]
    public class ItemData : ScriptableObject
    {
        [field: SerializeField]
        public string DisplayName { get; private set; }
        
        [field: SerializeField]
        public Sprite Icon { get; private set; }

        [field: SerializeField]
        public int StackSize { get; set; } = 1;
    }
}