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
        private struct SerializableStateInstance
        {
            public string name;
            public FsmState fsmState;
            public List<SerializableTransitionInstance> transitions;

            public static SerializableStateInstance Serialize (StateInstance stateInstance) {
                SerializableStateInstance serializableStateInstance = new SerializableStateInstance() {
                    name = stateInstance.name,
                    fsmState = stateInstance.state,
                    transitions = new List<SerializableTransitionInstance>()
                };

                foreach (TransitionInstance transitionInstance in stateInstance.transitions) {
                    serializableStateInstance.transitions.Add(SerializableTransitionInstance.Serialize(transitionInstance));
                }

                return serializableStateInstance;
            }

            public StateInstance Deserialize (IDictionary<string, FsmParameter> parameters) {
                StateInstance stateInstance = new StateInstance() {
                    name = name,
                    state = fsmState,
                    transitions = new List<TransitionInstance>()
                };

                foreach (SerializableTransitionInstance serializableFsmTransition in transitions) {
                    stateInstance.transitions.Add(serializableFsmTransition.Deserialize(parameters));
                }

                return stateInstance;
            }
        }

        [Serializable]
        private struct SerializableTransitionInstance
        {
            public SerializableFsmTransition fsmTransition;
            public string targetState;

            public static SerializableTransitionInstance Serialize (TransitionInstance transitionInstance) {
                return new SerializableTransitionInstance() {
                    targetState = transitionInstance.targetState,
                    fsmTransition = SerializableFsmTransition.Serialize(transitionInstance.transition)
                };
            }

            public TransitionInstance Deserialize (IDictionary<string, FsmParameter> parameters) {
                return new TransitionInstance() {
                    transition = fsmTransition.Deserialize(parameters),
                    targetState = targetState
                };
            }
        }

        [SerializeField] private List<SerializableStateInstance> serializableStateInstances = new List<SerializableStateInstance>();
        [SerializeField] private List<SerializableFsmParameter> serializableFsmParameters = new List<SerializableFsmParameter>();

        public void OnBeforeSerialize () {
            serializableStateInstances.Clear();
            serializableFsmParameters.Clear();

            foreach (KeyValuePair<string, StateInstance> kvp in fsm) {
                serializableStateInstances.Add(SerializableStateInstance.Serialize(kvp.Value));
            }

            foreach (KeyValuePair<string, FsmParameter> kvp in parameters) {
                serializableFsmParameters.Add(SerializableFsmParameter.Serialize(kvp.Key, kvp.Value));
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
            foreach (SerializableFsmParameter serializableFsmParameter in serializableFsmParameters) {
                // Prevent empty names
                if (serializableFsmParameter.name.Length == 0) {
                    serializableFsmParameter.name = "New parameter";
                }

                // Prevent duplicate names (not permitted by dictionary)
                while (parameters.ContainsKey(serializableFsmParameter.name)) {
                    serializableFsmParameter.name = IncrementName(serializableFsmParameter.name);
                }

                parameters.Add(serializableFsmParameter.name, serializableFsmParameter.Deserialize());
            }
        }

        private void DeserializeFsm () {
            foreach (SerializableStateInstance serializableStateInstance in serializableStateInstances) {
                // Prevent empty names
                string name = serializableStateInstance.name.Length == 0 ? "New state" : serializableStateInstance.name;

                // Prevent duplicate names (not permitted by dictionary)
                while (fsm.ContainsKey(name)) {
                    name = IncrementName(name);
                }

                fsm.Add(name, serializableStateInstance.Deserialize(parameters));
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
