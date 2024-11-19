namespace TheLastLand._Project.Scripts.Characters.Player.StateMachines.Movement.States.Grounded
{
    public class PlayerIdlingState : PlayerGroundedState
    {
        public PlayerIdlingState(PlayerMovementStateMachine playerMovement) : base(playerMovement)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            StateData.VelocityPower = ZeroF;
            ResetVelocity();
        }
    }
}