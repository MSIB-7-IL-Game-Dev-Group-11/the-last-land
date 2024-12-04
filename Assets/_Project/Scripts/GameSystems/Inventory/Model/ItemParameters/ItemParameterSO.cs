using UnityEngine;

namespace TheLastLand._Project.Scripts.GameSystems.Inventory.Model.ItemParameters
{
    [CreateAssetMenu]
    public class ItemParameterSo : ScriptableObject
    {
        [field: SerializeField]
        public string ParameterName { get; private set; }
    }
}