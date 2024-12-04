using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TheLastLand._Project.Scripts.GameSystems.Inventory.UI
{
    public class UIInventoryItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler,
        IEndDragHandler, IDropHandler, IDragHandler
    {
        [SerializeField]
        private Image itemImage;

        [SerializeField]
        private TMP_Text quantityTxt;

        [SerializeField]
        private Image borderImage;

        private bool _empty = true;

        public void Awake()
        {
            ResetData();
            Deselect();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_empty)
                return;
            OnItemBeginDrag?.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {
            // Implement drag functionality if needed.
        }

        public void OnDrop(PointerEventData eventData)
        {
            OnItemDroppedOn?.Invoke(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnItemEndDrag?.Invoke(this);
        }

        public void OnPointerClick(PointerEventData pointerData)
        {
            if (pointerData.button == PointerEventData.InputButton.Right)
                OnRightMouseBtnClick?.Invoke(this);
            else
                OnItemClicked?.Invoke(this);
        }

        public event Action<UIInventoryItem> OnItemClicked,
            OnItemDroppedOn,
            OnItemBeginDrag,
            OnItemEndDrag,
            OnRightMouseBtnClick;

        public void ResetData()
        {
            itemImage.gameObject.SetActive(false);
            _empty = true;
        }

        public void Deselect()
        {
            borderImage.enabled = false;
        }

        public void SetData(Sprite sprite, int quantity)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = sprite;
            quantityTxt.text = quantity.ToString();
            _empty = false;
        }

        public void Select()
        {
            borderImage.enabled = true;
        }
    }
}