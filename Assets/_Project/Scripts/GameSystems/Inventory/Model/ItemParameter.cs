using System;
using TheLastLand._Project.Scripts.GameSystems.Inventory.Model.ItemParameters;

namespace TheLastLand._Project.Scripts.GameSystems.Inventory.Model
{
    [Serializable]
    public struct ItemParameter : IEquatable<ItemParameter>
    {
        public ItemParameterSo itemParameter;
        public float value;

        public bool Equals(ItemParameter other)
        {
            return other.itemParameter == itemParameter;
        }
    }
}