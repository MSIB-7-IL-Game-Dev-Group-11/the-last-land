namespace TheLastLand._Project.Scripts.GameSystems.Item.Common
{
    /// <summary>
    /// Interface representing a slot item.
    /// </summary>
    public interface ISlotItem
    {
        /// <summary>
        /// Gets the index of the slot.
        /// </summary>
        int Index { get; }

        /// <summary>
        /// Gets the item in the slot.
        /// </summary>
        IItem Item { get; }

        /// <summary>
        /// Clears the slot.
        /// </summary>
        void ClearSlot();

        /// <summary>
        /// Draws the slot with the specified item.
        /// </summary>
        /// <param name="item">The item to draw in the slot.</param>
        void DrawSlot(IItem item);

        /// <summary>
        /// Selects or deselects the slot.
        /// </summary>
        /// <param name="isSelected">True if the slot is selected, false otherwise.</param>
        void SelectSlot(bool isSelected);
    }
}