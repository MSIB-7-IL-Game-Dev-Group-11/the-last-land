using System;
using UnityEngine;

namespace TheLastLand._Project.Scripts.Input
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class WallCheck : MonoBehaviour
    {
        [SerializeField] private float wallCheckRadius = 0.2f;
        [SerializeField] private LayerMask wallLayer;
        [SerializeField] private InputReader inputReader;

        private BoxCollider2D _boxCollider;
        
        private const float ZeroF = 0f;
        
        private float _direction;
        
        public bool IsTouching { get; private set; }

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            _direction = inputReader.Direction.x;
        }

        public void FixedUpdate()
        {
            var direction = new Vector2(_direction, 0);
            var distance = _boxCollider.bounds.extents.x + wallCheckRadius;
            var size = new Vector2(wallCheckRadius, _boxCollider.size.y);
        
            IsTouching = Physics2D.BoxCast(
                transform.position,
                size,
                ZeroF,
                direction,
                distance,
                wallLayer
            );
        }
    }
}