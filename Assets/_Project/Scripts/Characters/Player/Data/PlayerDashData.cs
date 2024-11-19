using System;
using UnityEngine;

namespace TheLastLand._Project.Scripts.Characters.Player.Data
{
    [Serializable]
    public class PlayerDashData
    {
        [field: SerializeField, Range(1f, 3f)]
        public float SpeedModifier { get; private set; } = 1.2f;
    }
}