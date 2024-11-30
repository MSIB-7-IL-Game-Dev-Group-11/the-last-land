using TheLastLand._Project.Scripts.GameSystems.Item;
using TheLastLand._Project.Scripts.GameSystems.Item.Common;
using UnityEngine;
using UnityEngine.Events;

namespace TheLastLand._Project.Scripts
{
    public class Item : MonoBehaviour, ICollectible
    {
        [SerializeField] private ItemData itemData;
        public event UnityAction<ItemData> OnItemCollected = delegate { };
        
        public void Collect()
        {
            Destroy(gameObject);
            OnItemCollected?.Invoke(itemData);
        }
    }
}