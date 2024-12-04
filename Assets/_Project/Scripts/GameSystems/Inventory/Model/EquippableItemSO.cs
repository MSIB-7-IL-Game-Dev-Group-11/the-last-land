using System.Collections.Generic;
using TheLastLand._Project.Scripts.GameSystems.Inventory.Common;
using UnityEngine;

namespace TheLastLand._Project.Scripts.GameSystems.Inventory.Model
{
    [CreateAssetMenu]
    public class EquippableItemSo : ItemSo, IDestroyableItem, IItemAction
    {
        public string ActionName => "Equip";

        [field: SerializeField]
        public AudioClip ActionSfx { get; private set; }

        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            var weaponSystem = character.GetComponent<AgentWeapon>();
            if (weaponSystem == null) return false;
            weaponSystem.SetWeapon(this, itemState ?? DefaultParametersList);
            return true;
        }
    }
}