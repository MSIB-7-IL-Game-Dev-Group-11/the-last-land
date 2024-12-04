using System.Collections.Generic;
using TheLastLand._Project.Scripts.GameSystems.Inventory.Common;
using UnityEngine;

namespace TheLastLand._Project.Scripts.GameSystems.Inventory.Model
{
    [CreateAssetMenu]
    public class EdibleItemSo : ItemSo, IDestroyableItem, IItemAction
    {
        [SerializeField]
        private List<ModifierData> modifiersData = new();

        public string ActionName => "Consume";

        [field: SerializeField]
        public AudioClip ActionSfx { get; private set; }

        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            foreach (var data in modifiersData)
                data.statModifier.AffectCharacter(character, data.value);
            return true;
        }
    }
}