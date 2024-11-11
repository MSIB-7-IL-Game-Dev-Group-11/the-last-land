using UnityEngine;

namespace TheLastLand._Project.Scripts.Input
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class GroundCheck : MonoBehaviour
    {
        [SerializeField] private float groundCheckRadius = 0.2f;
        [SerializeField] private LayerMask groundLayer;
        
        private const float ZeroF = 0f;
        
        private BoxCollider2D _boxCollider;
        
        public bool IsTouching { get; private set; }

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider2D>();
        }

        private void FixedUpdate()
        {
            var distance = _boxCollider.bounds.extents.y + groundCheckRadius;
            var size = new Vector2(_boxCollider.size.x, groundCheckRadius);
            
            IsTouching = Physics2D.BoxCast(
                transform.position,
                size,
                ZeroF,
                Vector2.down,
                distance,
                groundLayer
            );
        }
    }
}