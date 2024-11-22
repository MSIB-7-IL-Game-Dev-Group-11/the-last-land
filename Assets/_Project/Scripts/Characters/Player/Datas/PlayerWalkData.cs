using System;
using UnityEngine;

namespace TheLastLand._Project.Scripts.Characters.Player.Datas
{
    [Serializable]
    public class PlayerWalkData
    {
        [field: SerializeField, Range(0.0f, 10.0f)]
        public float Modifier { get; private set; } = 0.9f;
    }
}