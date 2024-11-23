using UnityEngine;

namespace TheLastLand._Project.Scripts.Characters.Player.StateMachines
{
    public class PlayerDashState : BaseState
    {
        public PlayerDashState(Scripts.Player player, Animator animator) : base(player, animator)
        {
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Player.Move(Player.Data.Dash.Modifier);
            Player.FlipCharacterSprite();
        }
    }
}