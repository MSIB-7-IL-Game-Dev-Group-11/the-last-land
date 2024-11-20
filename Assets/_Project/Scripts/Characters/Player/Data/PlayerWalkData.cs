using System;
using UnityEngine;

namespace TheLastLand._Project.Scripts.Characters.Player.Data
{
    [Serializable]
    public class PlayerWalkData
    {
        [field: SerializeField] 
        public float SpeedModifier { get; private set; } = 0.9f;
    }
}