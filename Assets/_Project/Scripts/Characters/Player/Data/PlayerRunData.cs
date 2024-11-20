using System;
using UnityEngine;

namespace TheLastLand._Project.Scripts.Characters.Player.Data
{
    [Serializable]
    public class PlayerRunData
    {
        [field: SerializeField] 
        public float SpeedModifier { get; private set; } = 1f;
    }
}