using UnityEngine;

namespace TheLastLand._Project.Scripts.Characters.Player.Datas
{
    public class PlayerStateData
    {
        /// <summary>
        /// The direction of the player's movement.
        /// </summary>
        public Vector2 MovementDirection;

        /// <summary>
        /// The current speed of the player.
        /// </summary>
        public float CurrentJumpVelocity;
        public float CurrentMoveVelocity;
        
        public bool IsWalking;
        public bool IsJumping;
        public bool IsDashing;
        
        public bool WasGrounded;
        public bool IsStaminaBelowMinimum;
    }
}