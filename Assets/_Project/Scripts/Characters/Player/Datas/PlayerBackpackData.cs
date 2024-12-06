using UnityEngine;

namespace TheLastLand._Project.Scripts.Characters.Player.Datas
{
    [System.Serializable]
    public class PlayerBackpackData
    {
        [field: SerializeField]
        public int Size { get; private set; } = 20;
    }
}