using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Helluys.FsmCore
{
    [CreateAssetMenu(fileName = "FiniteStateMachine", menuName = "FiniteStateMachine")]
    public partial class FiniteStateMachine : ScriptableObject
    {
        public class StateChangedEvent : UnityEvent<FsmState, FsmState> { }

        public class StateInstance
        {
            public string name;
            public FsmState state;
            public List<TransitionInstance> transitions;
        }

        public class TransitionInstance
        {
            public FsmTransition transition;
            public string targetState;
        }

        public StateChangedEvent OnStateChanged = new StateChangedEvent();
        public string currentStateInstanceName {  get { return currentStateInstance.name; } }
        public FsmState currentState { get { return currentStateInstance.state; } }

        private IDictionary<string, StateInstance> fsm;
        private IDictionary<string, FsmParameter> parameters;
        private StateInstance currentStateInstance;

        public IEnumerable<StateInstance> states { get { return fsm.Values; } }

        public FiniteStateMachine () {
            fsm = new Dictionary<string, StateInstance>();
            parameters = new Dictionary<string, FsmParameter>();
            currentStateInstance = null;
        }

        public void AddStateInstance (string name, FsmState state) {
            StateInstance newStateInstance = new StateInstance() {
                name = name,
                state = state,
                transitions = new List<TransitionInstance>()
            };
            
            fsm.Add(name, newStateInstance);
            
            if (currentStateInstance == null) {
                currentStateInstance = newStateInstance;
            }
        }

        public void RemoveState (string name) {
            if (fsm.Remove(name)) {
                // Remove all transitions pointing the the removed state
                foreach (StateInstance stateInstance in states) {
                    stateInstance.transitions.RemoveAll(t => t.targetState.Equals(name));
                }
            }
        }

        public void AddParameter (FsmParameter parameter) {
            parameters.Add(parameter.name, parameter);
        }

        public void RemoveParameter (string name) {
            if (parameters.TryGetValue(name, out FsmParameter removedParameter)) {
                // Remove all conditions using the removed parameter
                foreach (StateInstance stateInstance in states) {
                    foreach (TransitionInstance transitionInstance in stateInstance.transitions) {
                        transitionInstance.transition.RemoveParameter(removedParameter);
                    }
                }

                parameters.Remove(name);
            }
        }

        public IEnumerable<KeyValuePair<string, FsmParameter>> GetParameters() {
            return parameters;
        }

        public FsmParameter GetParameter (string name) {
            return parameters[name];
        }

        public void AddTransition (string fromState, string toState, FsmTransition transition) {
            if (!fsm.ContainsKey(toState)) {
                throw new KeyNotFoundException();
            }

            // Prevent duplicate transitions
            if (fsm[fromState].transitions.Exists(t => t.targetState.Equals(toState))) {
                throw new ArgumentException();
            }

            fsm[fromState].transitions.Add(new TransitionInstance() {
                targetState = toState,
                transition = transition
            });
        }

        public void RemoveTransition (string fromState, string toState) {
            fsm[fromState].transitions.RemoveAll(t => t.targetState.Equals(toState));
        }

        public void Update () {
            bool stateChanged = false;
            foreach (TransitionInstance transition in currentStateInstance.transitions) {
                if (transition.transition.Evaluate()) {
                    currentState.OnExit();

                    FsmState oldState = currentState;
                    currentStateInstance = fsm[transition.targetState];

                    currentState.OnEnter();

                    OnStateChanged.Invoke(oldState, currentState);

                    stateChanged = true;
                    break;
                }
            }

            if (!stateChanged) {
                currentState.OnStay();
            }
        }
    }
}
