using System.Collections.Generic;
using System.Linq;
using Enemy.States;
using UnityEngine;

namespace Enemy
{
    public class StateMachine
    {
        #region Properties
        private Dictionary<string, Base> _states = new Dictionary<string, Base>();
        private Base _activeState;
        #endregion

        #region Custom
        public bool AddState(Base state)
        {
            if (state == null || _states.ContainsKey(state.Name)) return false;
            _states.Add(state.Name, state);
            return true;
        }

        public bool SetActiveState<T>() where T : Base
        {
            // Find the first state in our dictionary that matches the Type T
            foreach (var state in _states.Values)
            {
                if (state is T)
                {
                    return SetActiveState(state);
                }
            }
            return false;
        }

        public bool SetActiveState(Base state)
        {
            if (state == null) return false;

            _activeState?.Exit();
            _activeState = state;
            _activeState.Enter();
            return true;
        }

        //public void Update() => _activeState?.Update();
        public void Update()
        {
            if (_activeState != null)
            {
                _activeState.Update();
            }
        }
        #endregion
    }
}