using System;
using TheLastLand._Project.Scripts.GameSystems.Item;
using TheLastLand._Project.Scripts.GameSystems.Item.Common;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TheLastLand._Project.Scripts.GameSystems
{
    public abstract class SlotItemBase : MonoBehaviour, ISlotItem, IBeginDragHandler, IDragHandler,
        IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public static event Action<int, int> ItemSwappedEvent = delegate { };

        public int Index { get; set; }
        public IItem Item { get; private set; }

        [SerializeField] protected Image icon;
        [SerializeField] protected TMP_Text amount;

        [SerializeField] protected GameObject itemContainer;
        [SerializeField] protected GameObject select;
        [SerializeField] protected GameObject counter;

        protected SlotUIManager SlotUIManager;
        private SlotItemBase _originalSlot;

        public virtual void ClearSlot()
        {
            Item = null;
            SlotUIManager?.ClearUI();
        }

        public virtual void SelectSlot(bool isSelected)
        {
            SlotUIManager.SetSelectionIndicator(isSelected);
        }

        public virtual void DrawSlot(IItem item)
        {
            if (item == null)
            {
                ClearSlot();
                return;
            }

            Item = item;
            SlotUIManager?.UpdateUI(item);
        }

        private void OnDisable()
        {
            SlotUIManager.ActivateSlotComponents(false);
        }

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            _originalSlot = SlotEventHandler.GetOriginalSlot(eventData);
            itemContainer.transform.SetParent(transform.root);
            itemContainer.transform.SetAsLastSibling();

            icon.raycastTarget = false;
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            if (_originalSlot == null || Item == null) return;

            itemContainer.transform.position = eventData.position;
            SlotUIManager.SetCounterActive(true);
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            itemContainer.transform.SetParent(transform);
            itemContainer.transform.localPosition = Vector3.zero;

            icon.raycastTarget = true;
            SlotUIManager.SetCounterActive(false);
        }

        public virtual void OnDrop(PointerEventData eventData)
        {
            var newSLot = SlotEventHandler.GetOriginalSlot(eventData);
            if (newSLot == null) return;

            SlotUIManager.ActivateSlotComponents();
            SwapItems(newSLot);
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            SlotEventHandler.HandleSlotHover(eventData, SlotUIManager);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            SlotUIManager.ActivateSlotComponents(false);
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