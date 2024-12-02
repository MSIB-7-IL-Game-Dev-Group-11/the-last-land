namespace TheLastLand._Project.Scripts.GameSystems.Item.Common
{
    public interface IStackable
    {
        int StackSize { get; }
        void AddToStack();
        void RemoveFromStack();
    }
}