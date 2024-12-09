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

        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text amount;

        [SerializeField] private GameObject itemContainer;
        [SerializeField] private GameObject select;
        [SerializeField] private GameObject counter;

        private SlotUIManager _slotUIManager;

        private void Awake()
        {
            _slotUIManager = new SlotUIManager(icon, amount, select, counter);
        }

        public virtual void ClearSlot()
        {
            Item = null;
            _slotUIManager?.ClearUI();
        }

        public virtual void SelectSlot(bool isSelected)
        {
            _slotUIManager.SetSelectionIndicator(isSelected);
        }

        public virtual void DrawSlot(IItem item)
        {
            if (item == null)
            {
                ClearSlot();
                return;
            }

            Item = item;
            _slotUIManager.UpdateUI(item);
        }

        public void OnDisable()
        {
            _slotUIManager.ActivateSlotComponents(false);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            itemContainer.transform.SetParent(transform.root);
            itemContainer.transform.SetAsLastSibling();

            icon.raycastTarget = false;
            _slotUIManager.SetCounterActive(true);
        }

        public void OnDrag(PointerEventData eventData)
        {
            itemContainer.transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            itemContainer.transform.SetParent(transform);
            itemContainer.transform.localPosition = Vector3.zero;

            icon.raycastTarget = true;
            _slotUIManager.SetCounterActive(false);
        }

        public void OnDrop(PointerEventData eventData)
        {
            var originalSlot = SlotEventHandler.GetOriginalSlot(eventData);
            if (originalSlot == null) return;

            _slotUIManager.ActivateSlotComponents();
            SwapItems(originalSlot);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            SlotEventHandler.HandleSlotHover(eventData, _slotUIManager);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _slotUIManager.ActivateSlotComponents(false);
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