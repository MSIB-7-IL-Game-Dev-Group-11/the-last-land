using System;
using TheLastLand._Project.Scripts.GameSystems.Inventory.Model.ItemModifiers;

namespace TheLastLand._Project.Scripts.GameSystems.Inventory.Model
{
    [Serializable]
    public class ModifierData
    {
        public CharacterStatModifierSo statModifier;
        public float value;
    }
}