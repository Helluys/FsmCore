using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace fsm {
    [CreateAssetMenu(fileName = "FiniteStateMachine", menuName = "FiniteStateMachine")]
    public partial class FiniteStateMachine : ScriptableObject {

        public class StateChangedEvent : UnityEvent<FsmState, FsmState> {}

        [Serializable]
        public class StateInstance {
            public FsmState state;
            public List<TransitionInstance> transitions = new List<TransitionInstance>();
        }

        [Serializable]
        public class TransitionInstance {
            public FsmTransition transition;
            public string targetState;
        }

        public StateChangedEvent OnStateChanged = new StateChangedEvent();
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

        public void AddState (string name, FsmState state) {
            fsm.Add(name, new StateInstance() {
                state = state
            });
        }

        public void RemoveState (string name) {
            if (fsm.Remove(name)) {
                // Remove all transitions pointing the the removed state
                foreach (StateInstance stateInstance in fsm.Values) {
                    stateInstance.transitions.RemoveAll(t => t.targetState.Equals(name));
                }
            }
        }

        public void AddParameter (string name, FsmParameter parameter) {
            parameters.Add(name, parameter);
        }

        public void RemoveParameter (string name) {
            if (parameters.TryGetValue(name, out FsmParameter removedParameter)) {
                // Remove all conditions using the removed parameter
                foreach (StateInstance stateInstance in fsm.Values) {
                    foreach (TransitionInstance transitionInstance in stateInstance.transitions) {
                        transitionInstance.transition.RemoveParameter(removedParameter);
                    }
                }

                parameters.Remove(name);
            }
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
