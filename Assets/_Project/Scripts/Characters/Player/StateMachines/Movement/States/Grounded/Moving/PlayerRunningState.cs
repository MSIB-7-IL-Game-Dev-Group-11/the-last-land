namespace TheLastLand._Project.Scripts.Characters.Player
    .StateMachines.Movement.States.Grounded.Moving
{
    public class PlayerRunningState : PlayerMovingState
    {
        public PlayerRunningState(PlayerMovementStateMachine playerMovement) : base(playerMovement)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            StateData.VelocityPower = MovementData.RunData.SpeedModifier;
        }
    }
}