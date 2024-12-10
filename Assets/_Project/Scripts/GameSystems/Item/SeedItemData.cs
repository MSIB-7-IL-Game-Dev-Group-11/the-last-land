using UnityEngine;

namespace TheLastLand._Project.Scripts.GameSystems.Item
{
    [CreateAssetMenu(fileName = "SeedItem", menuName = "TheLastLand/SeedItem")]
    public class SeedItemData : ItemData
    {
        [field: SerializeField]
        public GameObject PlantPrefab { get; private set; }
        
        [field: SerializeField]
        public GameObject SeedPrefab { get; private set; }
        
        [field: SerializeField]
        public GameObject HarvestedItemPrefab { get; private set; }

        [field: SerializeField]
        public float GrowTime { get; private set; }

        [field: SerializeField]
        public int MinimumHarvestAmount { get; private set; }

        [field: SerializeField]
        public int MaximumHarvestAmount { get; private set; }
        
        [field: SerializeField]
        public int MinimumDropSeedAmount { get; private set; }
        
        [field: SerializeField]
        public int MaximumDropSeedAmount { get; private set; }
    }
}