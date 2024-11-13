using System;
using System.Collections.Generic;
using System.Linq;

namespace TheLastLand._Project.Scripts.StateMachines
{
    public class StateMachine
    {
        private StateNode _currentState;
        private Dictionary<Type, StateNode> _nodes = new();
        private HashSet<ITransition> _allTransitions = new();

        public void Update()
        {
            var transition = GetTransition();

            if (transition != null)
            {
                ChangeState(transition.TargetState);
            }

            _currentState.State?.Update();
        }

        public void FixedUpdate()
        {
            _currentState.State?.FixedUpdate();
        }

        public void SetState(IState state)
        {
            _currentState = _nodes[state.GetType()];
            _currentState.State?.OnEnter();
        }

        private void ChangeState(IState state)
        {
            if (state == _currentState.State) return;

            var previousState = _currentState.State;
            var nextState = _nodes[state.GetType()].State;

            previousState?.OnExit();
            nextState?.OnEnter();

            _currentState = _nodes[state.GetType()];
        }

        private ITransition GetTransition()
        {
            foreach (var transition in _allTransitions)
            {
                if (transition.Condition.Evaluate())
                {
                    return transition;
                }
            }

            return _currentState.Transitions.FirstOrDefault(transition => transition.Condition.Evaluate());
        }
        
        
        public void AddTransition(IState from, IState targetState, IPredicate condition)
        {
            GetOrAddNode(from).AddTransition(GetOrAddNode(targetState).State , condition);
        }
        
        public void AddAnyTransition(IState targetState, IPredicate condition)
        {
            _allTransitions.Add(new Transition(GetOrAddNode(targetState).State, condition));
        }
        
        private StateNode GetOrAddNode(IState state)
        {
            if (_nodes.TryGetValue(state.GetType(), out var node))
            {
                return node;
            }

            node = new StateNode(state);
            _nodes.Add(state.GetType(), node);

           
            return node;
        }

        class StateNode
        {
            public IState State { get; }
            public HashSet<ITransition> Transitions { get; }

            public StateNode(IState state)
            {
                State = state;
                Transitions = new HashSet<ITransition>();
            }
            
            public void AddTransition(IState targetState, IPredicate condition)
            {
                Transitions.Add(new Transition(targetState, condition));
            }
        }
    }
}