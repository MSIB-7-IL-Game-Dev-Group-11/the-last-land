using Cinemachine;
using TheLastLand._Project.Scripts.Input;
using UnityEngine;

namespace TheLastLand._Project.Scripts.Characters.Player
{
    public class PlayerComponent
    {
        public InputReader PlayerInput { get; }
        public CinemachineVirtualCamera VirtualCamera { get; }

        public PlayerComponent(InputReader playerInput, CinemachineVirtualCamera virtualCamera)
        {
            PlayerInput = playerInput;
            VirtualCamera = virtualCamera;
        }
    }
}