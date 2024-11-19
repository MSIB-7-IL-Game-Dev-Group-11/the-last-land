using System;
using System.Collections.Generic;
using System.Linq;

namespace TheLastLand._Project.Scripts.StateMachines
{
    /// <summary>
    /// Represents a state machine that manages state transitions and updates.
    /// </summary>
    public class StateMachine
    {
        /// <summary>
        /// The current state node of the state machine.
        /// </summary>
        private StateNode _currentState;

        /// <summary>
        /// A dictionary mapping state types to their corresponding state nodes.
        /// </summary>
        private readonly Dictionary<Type, StateNode> _nodes = new();

        /// <summary>
        /// A set of all transitions that can occur from any state.
        /// </summary>
        private readonly HashSet<ITransition> _allTransitions = new();

        /// <summary>
        /// Updates the current state and handles state transitions.
        /// </summary>
        public void Update()
        {
            var transition = GetTransition();

            if (transition != null)
            {
                ChangeState(transition.TargetState);
            }

            _currentState.State?.Update();
        }

        /// <summary>
        /// Performs fixed updates on the current state.
        /// </summary>
        public void FixedUpdate()
        {
            _currentState.State?.FixedUpdate();
        }

        /// <summary>
        /// Sets the current state of the state machine.
        /// </summary>
        /// <param name="state">The state to set as the current state.</param>
        public void SetState(IState state)
        {
            _currentState = _nodes[state.GetType()];
            _currentState.State?.OnEnter();
        }

        /// <summary>
        /// Changes the current state to the specified state.
        /// </summary>
        /// <param name="state">The state to change to.</param>
        private void ChangeState(IState state)
        {
            if (state == _currentState.State) return;

            var previousState = _currentState.State;
            var nextState = _nodes[state.GetType()].State;

            previousState?.OnExit();
            nextState?.OnEnter();

            _currentState = _nodes[state.GetType()];
        }

        /// <summary>
        /// Gets the transition that should occur based on the current state and conditions.
        /// </summary>
        /// <returns>The transition to occur, or null if no transition should occur.</returns>
        private ITransition GetTransition()
        {
            foreach (var transition in _allTransitions)
            {
                if (transition.Condition.Evaluate())
                {
                    return transition;
                }
            }

            return _currentState.Transitions.FirstOrDefault(transition =>
                transition.Condition.Evaluate());
        }

        /// <summary>
        /// Adds a transition from one state to another with a specified condition.
        /// </summary>
        /// <param name="from">The state to transition from.</param>
        /// <param name="targetState">The state to transition to.</param>
        /// <param name="condition">The condition that must be met for the transition to occur.</param>
        public void AddTransition(IState from, IState targetState, IPredicate condition)
        {
            GetOrAddNode(from).AddTransition(GetOrAddNode(targetState).State, condition);
        }

        /// <summary>
        /// Adds a transition that can occur from any state to a specified state with a specified condition.
        /// </summary>
        /// <param name="targetState">The state to transition to.</param>
        /// <param name="condition">The condition that must be met for the transition to occur.</param>
        public void AddAnyTransition(IState targetState, IPredicate condition)
        {
            _allTransitions.Add(new Transition(GetOrAddNode(targetState).State, condition));
        }

        /// <summary>
        /// Gets or adds a state node for the specified state.
        /// </summary>
        /// <param name="state">The state to get or add a node for.</param>
        /// <returns>The state node for the specified state.</returns>
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

        /// <summary>
        /// Represents a node in the state machine, containing a state and its transitions.
        /// </summary>
        private class StateNode
        {
            /// <summary>
            /// Gets the state associated with this node.
            /// </summary>
            public IState State { get; }

            /// <summary>
            /// Gets the set of transitions from this state.
            /// </summary>
            public HashSet<ITransition> Transitions { get; }

            /// <summary>
            /// Initializes a new instance of the <see cref="StateNode"/> class.
            /// </summary>
            /// <param name="state">The state associated with this node.</param>
            public StateNode(IState state)
            {
                State = state;
                Transitions = new HashSet<ITransition>();
            }

            /// <summary>
            /// Adds a transition from this state to a target state with a specified condition.
            /// </summary>
            /// <param name="targetState">The target state to transition to.</param>
            /// <param name="condition">The condition that must be met for the transition to occur.</param>
            public void AddTransition(IState targetState, IPredicate condition)
            {
                Transitions.Add(new Transition(targetState, condition));
            }
        }
    }
}