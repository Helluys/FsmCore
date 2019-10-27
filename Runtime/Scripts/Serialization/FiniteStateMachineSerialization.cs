using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Helluys.FsmCore.Serialization;
using UnityEngine;

namespace Helluys.FsmCore
{
    public partial class FiniteStateMachine : ISerializationCallbackReceiver
    {
        [Serializable]
        private class SerializedStateInstance
        {
            public string name;
            public FsmState fsmState;
            public List<SerializedTransitionInstance> transitions;

            public static SerializedStateInstance Serialize (StateInstance stateInstance) {
                SerializedStateInstance serializedStateInstance = new SerializedStateInstance() {
                    name = stateInstance.name,
                    fsmState = stateInstance.state,
                    transitions = new List<SerializedTransitionInstance>()
                };

                foreach (TransitionInstance transitionInstance in stateInstance.transitions) {
                    serializedStateInstance.transitions.Add(SerializedTransitionInstance.Serialize(transitionInstance));
                }

                return serializedStateInstance;
            }

            public StateInstance Deserialize (IDictionary<string, FsmParameter> parameters) {
                StateInstance stateInstance = new StateInstance() {
                    name = name,
                    state = fsmState,
                    transitions = new List<TransitionInstance>()
                };

                foreach (SerializedTransitionInstance serializedFsmTransition in transitions) {
                    stateInstance.transitions.Add(serializedFsmTransition.Deserialize(parameters));
                }

                return stateInstance;
            }
        }

        [Serializable]
        private class SerializedTransitionInstance
        {
            public SerializedFsmTransition fsmTransition;
            public string targetState;

            public static SerializedTransitionInstance Serialize (TransitionInstance transitionInstance) {
                return new SerializedTransitionInstance() {
                    targetState = transitionInstance.targetState,
                    fsmTransition = SerializedFsmTransition.Serialize(transitionInstance.transition)
                };
            }

            public TransitionInstance Deserialize (IDictionary<string, FsmParameter> parameters) {
                return new TransitionInstance() {
                    transition = fsmTransition.Deserialize(parameters),
                    targetState = targetState
                };
            }
        }

        [SerializeField] private List<SerializedStateInstance> serializedStateInstances = new List<SerializedStateInstance>();
        [SerializeField] private List<SerializedFsmParameter> serializedFsmParameters = new List<SerializedFsmParameter>();

        public void OnBeforeSerialize () {
            serializedStateInstances.Clear();
            serializedFsmParameters.Clear();

            foreach (KeyValuePair<string, StateInstance> kvp in fsm) {
                serializedStateInstances.Add(SerializedStateInstance.Serialize(kvp.Value));
            }

            foreach (KeyValuePair<string, FsmParameter> kvp in parameters) {
                serializedFsmParameters.Add(SerializedFsmParameter.Serialize(kvp.Key, kvp.Value));
            }
        }

        public void OnAfterDeserialize () {
            fsm.Clear();
            parameters.Clear();

            // Deserialize parameters first (used in conditions)
            DeserializeParameters();
            DeserializeFsm();
        }

        private void DeserializeParameters () {
            foreach (SerializedFsmParameter serializedFsmParameter in serializedFsmParameters) {
                // Prevent empty names
                if (serializedFsmParameter.name.Length == 0) {
                    serializedFsmParameter.name = "New parameter";
                }

                // Prevent duplicate names (not permitted by dictionary)
                while (parameters.ContainsKey(serializedFsmParameter.name)) {
                    serializedFsmParameter.name = IncrementName(serializedFsmParameter.name);
                }

                parameters.Add(serializedFsmParameter.name, serializedFsmParameter.Deserialize());
            }
        }

        private void DeserializeFsm () {
            foreach (SerializedStateInstance serializedStateInstance in serializedStateInstances) {
                // Prevent empty names
                if (serializedStateInstance.name.Length == 0) {
                    serializedStateInstance.name  = "New state";
                }

                // Prevent duplicate names (not permitted by dictionary)
                while (fsm.ContainsKey(serializedStateInstance.name)) {
                    serializedStateInstance.name = IncrementName(serializedStateInstance.name);
                }

                fsm.Add(serializedStateInstance.name, serializedStateInstance.Deserialize(parameters));
            }
        }

        private static string IncrementName (string name) {
            Regex re = new Regex(@"([^\d]*)(\d*)$");
            Match result = re.Match(name);

            string alphaPart = result.Groups[1].Value;
            string numberPart = result.Groups[2].Value;
            int number = (numberPart.Length == 0) ? 0 : int.Parse(numberPart);

            return alphaPart + (number + 1);
        }
    }
}
