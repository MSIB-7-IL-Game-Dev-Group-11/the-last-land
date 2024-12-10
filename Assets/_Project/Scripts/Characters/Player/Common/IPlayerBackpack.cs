using TheLastLand._Project.Scripts.GameSystems.Backpack.Common;

namespace TheLastLand._Project.Scripts.Characters.Player.Common
{
    /// <summary>
    /// Interface representing a player's backpack.
    /// </summary>
    public interface IPlayerBackpack : IBackpackController
    {
        /// <summary>
        /// Gets the size of the backpack.
        /// </summary>
        int BackpackSize { get; }
    }
}