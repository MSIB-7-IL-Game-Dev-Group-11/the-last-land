using System;
using System.Collections.Generic;

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

        /// <summary>
        /// Gets the list of hotbar slots.
        /// </summary>
        IList<SlotItemBase> HotbarSlots { get; }

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
        /// Draws the hotbar UI.
        /// </summary>
        void DrawHotbar();

        /// <summary>
        /// Swaps two slots in the hotbar.
        /// </summary>
        /// <param name="from">The index of the first slot.</param>
        /// <param name="to">The index of the second slot.</param>
        void SwapSlot(int from, int to);
    }
}