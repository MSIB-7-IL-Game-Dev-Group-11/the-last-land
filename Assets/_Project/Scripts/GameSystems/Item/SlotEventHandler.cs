using UnityEngine.EventSystems;

namespace TheLastLand._Project.Scripts.GameSystems.Item
{
    public static class SlotEventHandler
    {
        public static SlotItemBase GetOriginalSlot(PointerEventData eventData)
        {
            var draggedItem = eventData.pointerDrag.GetComponent<SlotItemBase>();
            if (draggedItem == null || draggedItem.Item == null) return null;

            var originalSlot = draggedItem.GetComponentInParent<SlotItemBase>();
            if (originalSlot == null || originalSlot.Item == null) return null;

            return originalSlot;
        }

        public static void HandleSlotHover(PointerEventData eventData, SlotUIManager slotUIManager)
        {
            var item = eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotItemBase>()?.Item;

            if (item == null)
            {
                slotUIManager.SetCounterActive(false);
                slotUIManager.SetSelectionIndicator(true);
                return;
            }

            slotUIManager.ActivateSlotComponents();
        }
    }
}