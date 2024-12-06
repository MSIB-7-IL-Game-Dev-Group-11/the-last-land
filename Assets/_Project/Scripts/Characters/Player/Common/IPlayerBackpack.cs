using TheLastLand._Project.Scripts.GameSystems.Backpack.Common;

namespace TheLastLand._Project.Scripts.Characters.Player.Common
{
    public interface IPlayerBackpack : IBackpackController
    {
        int BackpackSize { get; }
    }
}