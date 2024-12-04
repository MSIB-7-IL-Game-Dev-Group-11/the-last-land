using System;
using System.Collections.Generic;
using UnityEngine;

namespace TheLastLand._Project.Scripts.GameSystems.Inventory.UI
{
    public class UIInventoryPage : MonoBehaviour
    {
        [SerializeField]
        private UIInventoryItem itemPrefab;

        [SerializeField]
        private RectTransform contentPanel;

        [SerializeField]
        private UIInventoryDescription itemDescription;

        [SerializeField]
        private MouseFollower mouseFollower;

        [SerializeField]
        private ItemActionPanels actionPanel;

        private readonly List<UIInventoryItem> _listOfUIItems = new();

        private int _currentlyDraggedItemIndex = -1;

        private void Awake()
        {
            Hide();
            mouseFollower.Toggle(false);
            itemDescription.ResetDescription();
        }

        public event Action<int> OnDescriptionRequested, OnItemActionRequested, OnStartDragging;

        public event Action<int, int> OnSwapItems;

        public void InitializeInventoryUI(int inventorySize)
        {
            for (var i = 0; i < inventorySize; i++)
            {
                var uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                _listOfUIItems.Add(uiItem);
                uiItem.OnItemClicked += HandleItemSelection;
                uiItem.OnItemBeginDrag += HandleBeginDrag;
                uiItem.OnItemDroppedOn += HandleSwap;
                uiItem.OnItemEndDrag += HandleEndDrag;
                uiItem.OnRightMouseBtnClick += HandleShowItemActions;
            }
        }

        internal void ResetAllItems()
        {
            foreach (var item in _listOfUIItems)
            {
                item.ResetData();
                item.Deselect();
            }
        }

        internal void UpdateDescription(int itemIndex, Sprite itemImage, string itemName,
            string description)
        {
            itemDescription.SetDescription(itemImage, itemName, description);
            DeselectAllItems();
            _listOfUIItems[itemIndex].Select();
        }

        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
        {
            if (_listOfUIItems.Count > itemIndex)
                _listOfUIItems[itemIndex].SetData(itemImage, itemQuantity);
        }

        private void HandleShowItemActions(UIInventoryItem inventoryItemUI)
        {
            var index = _listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1) return;
            OnItemActionRequested?.Invoke(index);
        }

        private void HandleEndDrag(UIInventoryItem inventoryItemUI)
        {
            ResetDraggedItem();
        }

        private void HandleSwap(UIInventoryItem inventoryItemUI)
        {
            var index = _listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1) return;
            OnSwapItems?.Invoke(_currentlyDraggedItemIndex, index);
            HandleItemSelection(inventoryItemUI);
        }

        private void ResetDraggedItem()
        {
            mouseFollower.Toggle(false);
            _currentlyDraggedItemIndex = -1;
        }

        private void HandleBeginDrag(UIInventoryItem inventoryItemUI)
        {
            var index = _listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
                return;
            _currentlyDraggedItemIndex = index;
            HandleItemSelection(inventoryItemUI);
            OnStartDragging?.Invoke(index);
        }

        public void CreateDraggedItem(Sprite sprite, int quantity)
        {
            mouseFollower.Toggle(true);
            mouseFollower.SetData(sprite, quantity);
        }

        private void HandleItemSelection(UIInventoryItem inventoryItemUI)
        {
            var index = _listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
                return;
            OnDescriptionRequested?.Invoke(index);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            ResetSelection();
        }

        public void ResetSelection()
        {
            itemDescription.ResetDescription();
            DeselectAllItems();
        }

        public void AddAction(string actionName, Action performAction)
        {
            actionPanel.AddButton(actionName, performAction);
        }

        public void ShowItemAction(int itemIndex)
        {
            actionPanel.Toggle(true);
            actionPanel.transform.position = _listOfUIItems[itemIndex].transform.position;
        }

        private void DeselectAllItems()
        {
            foreach (var item in _listOfUIItems) item.Deselect();
            actionPanel.Toggle(false);
        }

        public void Hide()
        {
            actionPanel.Toggle(false);
            gameObject.SetActive(false);
            ResetDraggedItem();
        }
    }
}