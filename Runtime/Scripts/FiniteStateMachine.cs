using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Helluys.FsmCore {

	[CreateAssetMenu(fileName = "FiniteStateMachine", menuName = "FiniteStateMachine")]
	public partial class FiniteStateMachine : ScriptableObject {
		public class StateChangedEvent : UnityEvent<FsmState, FsmState> { }

		[Serializable]
		public class StateInstance {
			public string name;
			public FsmState state;
			public List<TransitionInstance> transitions;
		}

		[Serializable]
		public class TransitionInstance {
			public FsmTransition transition;
			public string originState;
			public string targetState;
		}

		public StateChangedEvent OnStateChanged = new StateChangedEvent();
		public string currentStateInstanceName { get { return currentStateInstance.name; } }
		public FsmState currentState { get { return currentStateInstance.state; } }

		private List<StateInstance> _stateInstances;
		private List<FsmParameter> _parameters;
		private StateInstance currentStateInstance;

		public IEnumerable<StateInstance> stateInstances { get { return _stateInstances; } }
		public IEnumerable<FsmParameter> parameters { get { return _parameters; } }

		public FiniteStateMachine() {
			_stateInstances = new List<StateInstance>();
			_parameters = new List<FsmParameter>();
			currentStateInstance = null;
		}

		public StateInstance AddStateInstance(string name, FsmState state) {
			StateInstance si = new StateInstance() {
				name = name,
				state = state,
				transitions = new List<TransitionInstance>()
			};

			while(_stateInstances.Find(si2 => si2.name.Equals(name)) != null) {
				name = IncrementName(name);
			}

			_stateInstances.Add(si);

			if(currentStateInstance == null) {
				currentStateInstance = si;
			}

			return si;
		}

		public void RemoveStateInstance(string name) {
			if(_stateInstances.RemoveAll(si => si.name.Equals(name)) > 0) {
				// Remove all transitions pointing to the removed state
				foreach(StateInstance stateInstance in stateInstances) {
					stateInstance.transitions.RemoveAll(t => t.targetState.Equals(name));
				}
			}
		}

		public StateInstance GetStateInstance(string name) {
			StateInstance si = _stateInstances.Find(si2 => si2.name.Equals(name));
			if(si == null) {
				throw new KeyNotFoundException("State instance " + name + " does not exist");
			}

			return si;
		}

		public void AddParameter(FsmParameter parameter) {
			// Prevent duplicate names
			string name = parameter != null ? parameter.name : "New parameter";
			while(_parameters.Find(p => p.name.Equals(name)) != null) {
				name = IncrementName(name);
			}

			_parameters.Add(parameter);
		}

		public void RemoveParameter(string name) {
			FsmParameter parameter = GetParameter(name);

			// Remove all conditions using the removed parameter
			foreach(StateInstance stateInstance in stateInstances) {
				foreach(TransitionInstance transitionInstance in stateInstance.transitions) {
					transitionInstance.transition.RemoveParameter(parameter);
				}
			}

			_parameters.Remove(parameter);
		}

		public void RenameParameter(string oldName, string newName) {
			FsmParameter parameter = GetParameter(oldName);

			// Prevent duplicate names
			string name = newName;
			while(_parameters.Find(p => p.name.Equals(name)) != null && name != oldName) {
				name = IncrementName(name);
			}

			parameter.name = name;

			foreach(StateInstance si in stateInstances) {
				foreach(TransitionInstance transition in si.transitions) {
					foreach(FsmCondition condition in transition.transition) {
						if(condition.Contains(oldName)) {
							condition.ReplaceParameter(parameter);
						}
					}
				}
			}
		}

		public FsmParameter GetParameter(string name) {
			FsmParameter parameter = _parameters.Find(p => p.name.Equals(name));
			if(parameter == null) {
				throw new KeyNotFoundException("Parameter " + name + " does not exist");
			}
			return parameter;
		}

		public void AddTransition(string fromState, string toState, FsmTransition transition) {
			StateInstance si = GetStateInstance(fromState);

			// Prevent duplicate transitions
			if(si.transitions.Exists(t => t.targetState.Equals(toState))) {
				throw new ArgumentException("Transition from " + fromState + " to " + toState + " already exists");
			}

			si.transitions.Add(new TransitionInstance() {
				transition = transition,
				originState = fromState,
				targetState = toState
			});
		}

		public void RemoveTransition(string fromState, string toState) {
			GetStateInstance(fromState).transitions.RemoveAll(t => t.targetState.Equals(toState));
		}

		public FsmTransition GetTransition(string fromState, string toState) {
			return GetStateInstance(fromState).transitions.Find(t => t.targetState.Equals(toState))?.transition;
		}

		public void Update() {
			bool stateChanged = false;
			foreach(TransitionInstance transition in currentStateInstance.transitions) {
				if(transition.transition.Evaluate()) {
					currentState.OnExit();

					FsmState oldState = currentState;
					currentStateInstance = GetStateInstance(transition.targetState);

					currentState.OnEnter();

					OnStateChanged.Invoke(oldState, currentState);

					stateChanged = true;
					break;
				}
			}

			if(stateChanged) {
				Update();
			} else {
				currentState.OnStay();
			}
		}
	}
}
