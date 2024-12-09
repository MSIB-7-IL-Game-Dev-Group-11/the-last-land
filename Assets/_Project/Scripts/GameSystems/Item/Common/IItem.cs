namespace TheLastLand._Project.Scripts.GameSystems.Item.Common
{
    /// <summary>
    /// Interface representing an item.
    /// </summary>
    public interface IItem : IStackable
    {
        /// <summary>
        /// Gets the data of the item.
        /// </summary>
        ItemData ItemData { get; }

        /// <summary>
        /// Method to use the item.
        /// </summary>
        void Use();
    }
}