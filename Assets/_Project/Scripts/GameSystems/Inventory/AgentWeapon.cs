using System.Collections.Generic;
using TheLastLand._Project.Scripts.GameSystems.Inventory.Model;
using UnityEngine;

namespace TheLastLand._Project.Scripts.GameSystems.Inventory
{
    public class AgentWeapon : MonoBehaviour
    {
        [SerializeField]
        private EquippableItemSo weapon;

        [SerializeField]
        private InventorySo inventoryData;

        [SerializeField]
        private List<ItemParameter> parametersToModify, itemCurrentState;

        public void SetWeapon(EquippableItemSo weaponItemSo, List<ItemParameter> itemState)
        {
            if (weapon != null) inventoryData.AddItem(weapon, 1, itemCurrentState);

            weapon = weaponItemSo;
            itemCurrentState = new List<ItemParameter>(itemState);
            ModifyParameters();
        }

        private void ModifyParameters()
        {
            foreach (var parameter in parametersToModify)
                if (itemCurrentState.Contains(parameter))
                {
                    var index = itemCurrentState.IndexOf(parameter);
                    var newValue = itemCurrentState[index].value + parameter.value;
                    itemCurrentState[index] = new ItemParameter
                    {
                        itemParameter = parameter.itemParameter,
                        value = newValue
                    };
                }
        }
    }
}