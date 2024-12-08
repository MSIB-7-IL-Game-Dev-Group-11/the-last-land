namespace TheLastLand._Project.Scripts.GameSystems.Item.Common
{
    public interface IItem : IStackable
    {
        ItemData ItemData { get; }
        void Use();
    }
}