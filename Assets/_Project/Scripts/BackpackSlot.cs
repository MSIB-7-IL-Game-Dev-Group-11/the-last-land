using TheLastLand._Project.Scripts.GameSystems;
using TheLastLand._Project.Scripts.GameSystems.Item;

namespace TheLastLand._Project.Scripts
{
    public class BackpackSlot : SlotItemBase
    {
        private void Awake()
        {
            SlotUIManager = new SlotUIManager(icon, amount, select, counter);
        }
    }
}