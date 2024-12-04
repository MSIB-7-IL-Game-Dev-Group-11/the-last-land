using TheLastLand._Project.Scripts.GameSystems.Inventory.Model;
using UnityEngine;

namespace TheLastLand._Project.Scripts.GameSystems.Inventory.PickUpSystem
{
    public class PickUpSystem : MonoBehaviour
    {
        [SerializeField]
        private InventorySo inventoryData;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var item = collision.GetComponent<Item>();
            if (item == null) return;
            
            var reminder = inventoryData.AddItem(item.InventoryItem, item.Quantity);
            if (reminder == 0)
                item.DestroyItem();
            else
                item.Quantity = reminder;
        }
    }
}