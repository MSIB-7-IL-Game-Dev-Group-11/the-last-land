using System;
using TheLastLand._Project.Scripts.Utils;
using UnityEngine;

namespace TheLastLand._Project.Scripts.Characters.Player.Datas
{
    [Serializable]
    public class PlayerDashData
    {
        [field: SerializeField]
        public float Force { get; private set; } = 10f;

        [field: SerializeField, ReadOnlyInPlayMode]
        public float Cooldown { get; private set; } = 0.5f;

        [field: SerializeField, ReadOnlyInPlayMode]
        public float Duration { get; private set; } = 0.5f;
        
        [field: SerializeField, Range(0.0f, 10.0f)]
        public float Modifier { get; private set; } = 1.0f;
    }
}