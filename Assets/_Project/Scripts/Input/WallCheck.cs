using UnityEngine;
using UnityEngine.InputSystem;

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

        private void OnEnable()
        {
            inputReader.Move += OnMove;
        }

        private void OnDisable()
        {
            inputReader.Move -= OnMove;
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            _direction = context.ReadValue<Vector2>().x;
        }

        public void FixedUpdate()
        {
            IsTouching = Physics2D.BoxCast(
                _boxCollider.bounds.center,
                _boxCollider.bounds.size,
                ZeroF,
                Vector2.right * _direction,
                wallCheckRadius,
                wallLayer
            );
        }
    }
}