using Cinemachine;
using TheLastLand._Project.Scripts.Input;
using UnityEngine;

namespace TheLastLand._Project.Scripts.Characters.Player
{
    public class PlayerComponent
    {
        public InputReader PlayerInput { get; }
        public CinemachineVirtualCamera VirtualCamera { get; }
        public Animator Animator { get; }
        public SpriteRenderer CharacterSprite { get; }
        public Rigidbody2D Rigidbody { get; }
        public GroundCheck GroundCheck { get; }

        public PlayerComponent(InputReader playerInput, CinemachineVirtualCamera virtualCamera,
            Animator animator, SpriteRenderer characterSprite, Rigidbody2D rigidbody,
            GroundCheck groundCheck)
        {
            PlayerInput = playerInput;
            VirtualCamera = virtualCamera;
            Animator = animator;
            CharacterSprite = characterSprite;
            Rigidbody = rigidbody;
            GroundCheck = groundCheck;
        }
    }
}