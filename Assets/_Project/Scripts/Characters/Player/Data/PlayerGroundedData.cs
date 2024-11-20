using System;
using UnityEngine;

namespace TheLastLand._Project.Scripts.Characters.Player.Data
{
    [Serializable]
    public class PlayerGroundedData
    {
        [field: SerializeField, Range(0f, 600f)]
        public float BaseSpeed { get; private set; } = 300f;

        [field: SerializeField, Range(0f, 100)]
        public float Acceleration { get; private set; } = 5.8f;

        [field: SerializeField]
        public PlayerWalkData WalkData { get; private set; }

        [field: SerializeField]
        public PlayerRunData RunData { get; private set; }
        
        
    }
}