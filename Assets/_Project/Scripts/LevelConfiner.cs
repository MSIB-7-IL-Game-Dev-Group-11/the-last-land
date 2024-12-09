using System;
using UnityEngine;

namespace TheLastLand._Project.Scripts
{
    public class LevelConfiner : MonoBehaviour
    {
        public static event Action<Collider2D> OnConfinerEnter = delegate { };

        private void Start()
        {
            OnConfinerEnter?.Invoke(GetComponent<PolygonCollider2D>());
        }
    }
}