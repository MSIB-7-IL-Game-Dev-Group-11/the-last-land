using TheLastLand._Project.Scripts.GameSystems.Item;
using TheLastLand._Project.Scripts.GameSystems.Item.Common;

namespace TheLastLand._Project.Scripts.GameSystems.Backpack.Common
{
    public interface IBackpackItem : IStackable
    {
        ItemData ItemData { get; }
    }
}