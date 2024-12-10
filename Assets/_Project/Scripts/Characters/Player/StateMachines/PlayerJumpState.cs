namespace TheLastLand._Project.Scripts.Characters.Player.StateMachines
{
    public class PlayerJumpState : BaseState
    {
        public override void FixedUpdate()
        {
            PlayerController.Jump();
            PlayerController.Move(PlayerData.Walk.Modifier);
            PlayerController.FlipCharacterSprite();
        }
    }
}