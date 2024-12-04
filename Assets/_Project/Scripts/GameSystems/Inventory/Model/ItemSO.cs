using System.Collections.Generic;
using UnityEngine;

namespace TheLastLand._Project.Scripts.GameSystems.Inventory.Model
{
    public abstract class ItemSo : ScriptableObject
    {
        [field: SerializeField]
        public bool IsStackable { get; set; }

        [field: SerializeField]
        public int MaxStackSize { get; set; } = 1;

        [field: SerializeField]
        public string Name { get; set; }

        [field: SerializeField]
        [field: TextArea]
        public string Description { get; set; }

        [field: SerializeField]
        public Sprite ItemImage { get; set; }

        [field: SerializeField]
        public List<ItemParameter> DefaultParametersList { get; set; }

        public int ID => GetInstanceID();
    }
}