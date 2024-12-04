using UnityEngine;

namespace TheLastLand._Project.Scripts.GameSystems.Inventory.Model.ItemModifiers
{
    public abstract class CharacterStatModifierSo : ScriptableObject
    {
        public abstract void AffectCharacter(GameObject character, float val);
    }
}