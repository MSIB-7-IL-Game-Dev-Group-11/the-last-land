using UnityEngine;

namespace TheLastLand._Project.Scripts.Characters.Player.Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "TheLastLand/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        [field:Header("Movement Settings")]
        [field: SerializeField] 
        public PlayerGroundedData GroundedData { get; private set; }

        [Header("Jump Settings")]
        public float jumpForce = 25f;
        public float jumpCooldown = 0.5f;
        public float jumpDuration = 0.2f;
        public float gravityMultiplier = 10f;
        
        
    }
}