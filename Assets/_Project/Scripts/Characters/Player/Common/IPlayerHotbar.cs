using TheLastLand._Project.Scripts.GameSystems.Hotbar.Common;

namespace TheLastLand._Project.Scripts.Characters.Player.Common
{
    public interface IPlayerHotbar : IHotbarController
    {
        /// <summary>
        /// Gets the size of the backpack.
        /// </summary>
        int HotbarSize { get; }
    }
}