using System;
using TheLastLand._Project.Scripts.GameSystems.Item.Common;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TheLastLand._Project.Scripts.GameSystems
{
    public abstract class SlotItemBase : MonoBehaviour, ISlotItem, IBeginDragHandler, IDragHandler,
        IEndDragHandler, IDropHandler
    {
        public static event Action<int, int> ItemSwappedEvent = delegate { };

        public int Index { get; set; }
        public IItem Item { get; private set; }

        [SerializeField] protected Image icon;
        [SerializeField] protected TMP_Text amount;

        private Transform _itemTransform;

        public virtual void ClearSlot()
        {
            Item = null;
            icon.enabled = false;
            amount.enabled = false;
        }

        public virtual void SelectSlot(bool isSelected)
        {
            // // Update the UI to indicate the selection state
            // // For example, change the background color or add a border
            // var selectionIndicator = transform.Find("SelectionIndicator");
            // if (selectionIndicator != null)
            // {
            //     selectionIndicator.gameObject.SetActive(isSelected);
            // }
        }

        public virtual void DrawSlot(IItem item)
        {
            if (item == null)
            {
                ClearSlot();
                return;
            }

            Item = item;

            icon.enabled = true;
            amount.enabled = true;

            icon.sprite = item.ItemData.Icon;
            amount.SetText(item.StackSize.ToString());
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _itemTransform = transform.Find("Item");
            _itemTransform.SetParent(transform.root);
            _itemTransform.SetAsLastSibling();
            icon.raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _itemTransform.transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _itemTransform.SetParent(transform);
            _itemTransform.localPosition = Vector3.zero;
            icon.raycastTarget = true;
        }

        public void OnDrop(PointerEventData eventData)
        {
            var draggedItem = eventData.pointerDrag.GetComponent<SlotItemBase>();
            if (draggedItem == null || draggedItem.Item == null) return;

            var originalSlot = draggedItem.GetComponentInParent<SlotItemBase>();
            if (originalSlot == null || originalSlot.Item == null) return;

            SwapItems(originalSlot);
        }

        private void SwapItems(SlotItemBase otherSlot)
        {
            var tempItem = Item;
            DrawSlot(otherSlot.Item);
            otherSlot.DrawSlot(tempItem);

            ItemSwappedEvent?.Invoke(otherSlot.Index, Index);
        }
    }
}