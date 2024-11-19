using UnityEngine;

namespace TheLastLand._Project.Scripts.Characters.Player.Data
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
        public float CurrentSpeed;
        
        public float VelocityPower;

        /// <summary>
        /// Indicates whether the player is walking.
        /// </summary>
        public bool IsWalking;

        /// <summary>
        /// Indicates whether the player is running.
        /// </summary>
        public bool IsRunning;
    }
}