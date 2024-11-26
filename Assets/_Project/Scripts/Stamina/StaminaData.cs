using TheLastLand._Project.Scripts.Utils;
using UnityEngine;

namespace TheLastLand._Project.Scripts.Stamina
{
    [System.Serializable]
    public class StaminaData
    {
        [field: SerializeField, Range(0f, 200f), Tooltip("Default: 100")]
        public float Max { get; private set; } = 100f;
        
        [field: SerializeField, Min(0f)]
        public float Min { get; private set; }
        
        [field: SerializeField]
        public float RegenerationRate { get; private set; }
        
        [field: SerializeField, ReadOnlyInPlayMode]
        public float RegenerationTime { get; private set; }
    }
}