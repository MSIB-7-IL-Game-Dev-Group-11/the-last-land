using TheLastLand._Project.Scripts.GameSystems.Backpack.Common;
using UnityEngine;

namespace TheLastLand._Project.Scripts
{
    public class HotbarSlot : MonoBehaviour
    {
        private IBackpackItem _item;

        public void ClearSlot()
        {
            _item = null;
            // Update UI to show empty slot
        }

        public void SetItem(IBackpackItem item)
        {
            _item = item;
            // Update UI to show item
        }

        public IBackpackItem GetItem()
        {
            return _item;
        }

        public void SetSelected(bool isSelected)
        {
            // Update UI to show selection state
        }
    }
}