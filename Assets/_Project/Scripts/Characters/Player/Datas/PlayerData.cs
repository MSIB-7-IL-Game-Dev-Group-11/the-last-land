using UnityEngine;

namespace TheLastLand._Project.Scripts.Characters.Player.Datas
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "TheLastLand/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        [field: Header("Movement Settings")]
        [field: SerializeField, Range(0f, 1000f)]
        public float BaseSpeed { get; private set; } = 300f;

        [field: SerializeField, Range(0f, 10.0f)]
        public float Acceleration { get; private set; } = 5.6f;
        
        [field: SerializeField, Range(0f, 10.0f)]
        public float Deceleration { get; private set; } = 5.6f;
        
        [field: SerializeField, Range(0.0f, 200.0f)]
        public float Stamina { get; private set; }

        [field: SerializeField]
        public PlayerWalkData Walk { get; private set; }

        [field: SerializeField]
        public PlayerJumpData Jump { get; private set; }
        
        [field: SerializeField]
        public PlayerDashData Dash { get; private set; }
    }
}