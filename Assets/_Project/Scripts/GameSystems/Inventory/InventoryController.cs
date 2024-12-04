using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheLastLand._Project.Scripts.GameSystems.Inventory.Common;
using TheLastLand._Project.Scripts.GameSystems.Inventory.Model;
using TheLastLand._Project.Scripts.GameSystems.Inventory.UI;
using UnityEngine;

namespace TheLastLand._Project.Scripts.GameSystems.Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField]
        private UIInventoryPage inventoryUI;

        [SerializeField]
        private InventorySo inventoryData;

        public List<InventoryItem> initialItems = new();

        [SerializeField]
        private AudioClip dropClip;

        [SerializeField]
        private AudioSource audioSource;

        private void Start()
        {
            PrepareUI();
            PrepareInventoryData();
        }

        public void Update()
        {
            if (!UnityEngine.Input.GetKeyDown(KeyCode.I)) return;
            if (inventoryUI.isActiveAndEnabled == false)
            {
                inventoryUI.Show();
                foreach (var item in inventoryData.GetCurrentInventoryState())
                    inventoryUI.UpdateData(
                        item.Key,
                        item.Value.item.ItemImage,
                        item.Value.quantity
                    );
            }
            else
            {
                inventoryUI.Hide();
            }
        }

        private void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach (var item in initialItems.Where(item => !item.IsEmpty))
                inventoryData.AddItem(item);
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryUI.ResetAllItems();
            foreach (var item in inventoryState)
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
        }

        private void PrepareUI()
        {
            inventoryUI.InitializeInventoryUI(inventoryData.Size);
            inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryUI.OnSwapItems += HandleSwapItems;
            inventoryUI.OnStartDragging += HandleDragging;
            inventoryUI.OnItemActionRequested += HandleItemActionRequest;
        }

        private void HandleItemActionRequest(int itemIndex)
        {
            var inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            if (inventoryItem.item is IItemAction itemAction)
            {
                inventoryUI.ShowItemAction(itemIndex);
                inventoryUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
            }

            if (inventoryItem.item is IDestroyableItem)
                inventoryUI.AddAction("Drop", () => DropItem(itemIndex, inventoryItem.quantity));
        }

        private void DropItem(int itemIndex, int quantity)
        {
            inventoryData.RemoveItem(itemIndex, quantity);
            inventoryUI.ResetSelection();
            audioSource.PlayOneShot(dropClip);
        }

        private void PerformAction(int itemIndex)
        {
            var inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            if (inventoryItem.item is IDestroyableItem) inventoryData.RemoveItem(itemIndex, 1);

            if (inventoryItem.item is not IItemAction itemAction) return;

            itemAction.PerformAction(gameObject, inventoryItem.itemState);
            audioSource.PlayOneShot(itemAction.ActionSfx);
            if (inventoryData.GetItemAt(itemIndex).IsEmpty)
                inventoryUI.ResetSelection();
        }

        private void HandleDragging(int itemIndex)
        {
            var inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
        }

        private void HandleSwapItems(int itemIndex1, int itemIndex2)
        {
            inventoryData.SwapItems(itemIndex1, itemIndex2);
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            var inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                inventoryUI.ResetSelection();
                return;
            }

            var item = inventoryItem.item;
            var description = PrepareDescription(inventoryItem);
            inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.name, description);
        }

        private string PrepareDescription(InventoryItem inventoryItem)
        {
            var sb = new StringBuilder();
            sb.Append(inventoryItem.item.Description);
            sb.AppendLine();
            for (var i = 0; i < inventoryItem.itemState.Count; i++)
            {
                sb.Append(
                    $"{inventoryItem.itemState[i].itemParameter.ParameterName} "
                    + $": {inventoryItem.itemState[i].value} / "
                    + $"{inventoryItem.item.DefaultParametersList[i].value}"
                );
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}