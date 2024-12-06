namespace TheLastLand._Project.Scripts.GameSystems.Item.Common
{
    public interface IStackable
    {
        int StackSize { get; }
        void AddToStack(int stackSize);
        void RemoveFromStack(int stackSize);
    }
}