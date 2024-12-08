namespace TheLastLand._Project.Scripts.GameSystems.Item.Common
{
    public interface ISlotItem
    {
        int Index { get; }
        IItem Item { get; }
        void ClearSlot();
        void DrawSlot(IItem item);
        void SelectSlot(bool isSelected);
    }
}