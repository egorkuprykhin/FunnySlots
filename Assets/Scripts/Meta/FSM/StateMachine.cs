using System;
using System.Collections.Generic;
using UnityEngine.Assertions;
using VContainer;

namespace FSM
{
    public class StateMachine
    {
        [Inject] private readonly IObjectResolver _diContainer;

        private readonly Dictionary<Type, IState> _states = new();
        private IState _activeState;

        public void Enter<TState>() where TState: class, IState
        {
            IState state = GetState<TState>();
            
            ProcessActiveState(state);
            
            state?.Enter();
        }

        private TState GetState<TState>() where TState: class, IState
        {
            if (_states.ContainsKey(typeof(TState)))
                return (TState)_states[typeof(TState)];

            TState state = _diContainer.Resolve<TState>();
            
            Assert.IsNotNull(state);
            
            if (!_states.ContainsKey(typeof(TState)))
                _states.Add(typeof(TState), state);
            
            return state;
        }

        private void ProcessActiveState(IState state)
        {
            if (_activeState == state) 
                return;
            
            _activeState?.Exit();
            _activeState = state;
        }
    }
}