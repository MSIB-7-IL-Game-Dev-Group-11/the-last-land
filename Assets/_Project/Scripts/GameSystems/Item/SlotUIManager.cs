using TheLastLand._Project.Scripts.GameSystems.Item.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheLastLand._Project.Scripts.GameSystems.Item
{
    public class SlotUIManager
    {
        private readonly Image _icon;
        private readonly TMP_Text _amount;
        private readonly GameObject _select;
        private readonly GameObject _counter;

        public SlotUIManager(Image icon, TMP_Text amount, GameObject select, GameObject counter)
        {
            _icon = icon;
            _amount = amount;
            _select = select;
            _counter = counter;
        }

        public void ClearUI()
        {
            _icon.enabled = false;
            _amount.enabled = false;
        }

        public void SetSelectionIndicator(bool isSelected)
        {
            _select.SetActive(isSelected);
        }

        public void UpdateUI(IItem item)
        {
            _icon.enabled = true;
            _amount.enabled = true;

            _icon.sprite = item.ItemData.Icon;
            _amount.SetText(item.StackSize.ToString());
        }

        public void ActivateSlotComponents(bool active = true)
        {
            _select.SetActive(active);
            _counter.SetActive(active);
        }

        public void SetCounterActive(bool active)
        {
            _counter.SetActive(active);
        }
    }
}