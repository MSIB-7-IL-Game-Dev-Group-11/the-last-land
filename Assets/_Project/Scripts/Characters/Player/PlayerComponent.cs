using TheLastLand._Project.Scripts.Input;

namespace TheLastLand._Project.Scripts.Characters.Player
{
    public class PlayerComponent
    {
        public InputReader PlayerInput { get; }
        public PlayerComponent(InputReader playerInput)
        {
            PlayerInput = playerInput;
        }
    }
}