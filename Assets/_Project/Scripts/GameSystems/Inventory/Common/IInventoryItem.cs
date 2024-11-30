namespace TheLastLand._Project.Scripts.GameSystems.Inventory.Common
{
    public interface IInventoryItem
    {
        int StackSize { get; }
        void AddToStack();
        void RemoveFromStack();
    }
}