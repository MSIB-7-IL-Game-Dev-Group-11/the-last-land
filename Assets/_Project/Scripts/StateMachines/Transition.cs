﻿using TheLastLand._Project.Scripts.StateMachines.Common;

namespace TheLastLand._Project.Scripts.StateMachines
{
    public class Transition : ITransition
    {
        public IState TargetState { get; }
        public IPredicate Condition { get; }

        public Transition(IState targetState, IPredicate condition)
        {
            TargetState = targetState;
            Condition = condition;
        }
    }
}