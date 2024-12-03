using UnityEngine;

namespace TheLastLand._Project.Scripts.GameSystems.Health
{
    [System.Serializable]
    public class HealthData
    {
        [field: SerializeField, Tooltip("Default: 200")]
        public float Max { get; private set; } = 200;
        
        [field: SerializeField, Min(0f)]
        public float Min { get; private set; }
        
        [field: SerializeField]
        public float RegenerationRate { get; private set; }
        
        [field: SerializeField]
        public float RegenerationTime { get; private set; }
    }
}