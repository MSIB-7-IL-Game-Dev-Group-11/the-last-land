namespace TheLastLand._Project.Scripts.Characters.Player.StateMachines.Movement.States.Grounded.
    Moving
{
    public class PlayerWalkingState : PlayerMovingState
    {
        public PlayerWalkingState(PlayerMovementStateMachine playerMovement) : base(playerMovement)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            StateData.VelocityPower = MovementData.WalkData.SpeedModifier;
        }
    }
}