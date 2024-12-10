using System;
using TheLastLand._Project.Scripts.GameSystems.Item.Common;

namespace TheLastLand._Project.Scripts.GameSystems.Hotbar.Common
{
    /// <summary>
    /// Interface for the Hotbar Controller.
    /// </summary>
    public interface IHotbarController
    {
        /// <summary>
        /// Gets the index of the currently selected slot.
        /// </summary>
        int SelectedSlotIndex { get; }

        int LastSelectedSlotIndex { get; }
        
        IItem SelectedItem { get; set; }

        /// <summary>
        /// Initializes the hotbar controller with a callback function.
        /// </summary>
        /// <param name="initCallback">The callback function to be called during initialization.</param>
        void Initialize(Action<int> initCallback);

        /// <summary>
        /// Selects a slot in the hotbar.
        /// </summary>
        /// <param name="slotIndex">The index of the slot to select.</param>
        void SelectSlot(int slotIndex);

        /// <summary>
        /// Checks if the given index is a valid slot index.
        /// </summary>
        /// <returns>True if the index is valid, false otherwise.</returns>
        bool IsValidSlotIndex(int index);
    }
}