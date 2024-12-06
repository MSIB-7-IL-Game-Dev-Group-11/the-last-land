using System;
using TheLastLand._Project.Scripts.GameSystems.Backpack.Common;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TheLastLand._Project.Scripts
{
    public class BackpackSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,
        IDropHandler
    {
        public static event Action<int, int> ItemSwappedEvent = delegate { };

        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text amount;

        private Transform _itemParentAfterDrag;
        private Transform _itemTransform;

        private IBackpackItem _item;

        public void ClearSlot()
        {
            _item = null;
            icon.enabled = false;
            amount.enabled = false;
        }

        public void DrawSlot(IBackpackItem item)
        {
            if (item == null)
            {
                ClearSlot();
                return;
            }

            _item = item;

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
            icon.raycastTarget = true;
        }

        public void OnDrop(PointerEventData eventData)
        {
            var draggedItem = eventData.pointerDrag.GetComponent<BackpackSlot>();
            if (draggedItem == null) return;

            var originalSlot = draggedItem.GetComponentInParent<BackpackSlot>();
            if (originalSlot == null) return;

            SwapItems(originalSlot);
        }

        private void SwapItems(BackpackSlot otherSlot)
        {
            var tempItem = _item;
            DrawSlot(otherSlot._item);
            otherSlot.DrawSlot(tempItem);

            ItemSwappedEvent?.Invoke(
                transform.GetSiblingIndex(),
                otherSlot.transform.GetSiblingIndex()
            );
        }
    }
}