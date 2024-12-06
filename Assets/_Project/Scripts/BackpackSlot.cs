using TheLastLand._Project.Scripts.GameSystems.Backpack.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheLastLand._Project.Scripts
{
    public class BackpackSlot : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text amount;

        public void ClearSlot()
        {
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
            
            icon.enabled = true;
            amount.enabled = true;
            
            icon.sprite = item.ItemData.Icon;
            amount.SetText(item.StackSize.ToString());
        }
    }
}