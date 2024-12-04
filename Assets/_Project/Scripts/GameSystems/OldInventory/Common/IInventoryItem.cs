using TheLastLand._Project.Scripts.GameSystems.Item;
using TheLastLand._Project.Scripts.GameSystems.Item.Common;

namespace TheLastLand._Project.Scripts.GameSystems.OldInventory.Common
{
    public interface IInventoryItem : IStackable
    {
        ItemData ItemData { get; }
    }
}