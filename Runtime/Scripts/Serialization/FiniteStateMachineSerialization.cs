using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace fsm {
    public partial class FiniteStateMachine : ISerializationCallbackReceiver {

        [Serializable]
        private class SerializableStateInstance {
            public string name;
            public FsmState state;
            public List<SerializableFsmTransition> transitions = new List<SerializableFsmTransition>();
        }

        [SerializeField] private List<SerializableStateInstance> serializableStateInstances = new List<SerializableStateInstance>();
        [SerializeField] private List<SerializableFsmParameter> serializableFsmParameters = new List<SerializableFsmParameter>();

        public void OnBeforeSerialize () {
            serializableStateInstances.Clear();
            serializableFsmParameters.Clear();

            foreach (KeyValuePair<string, StateInstance> kvp in fsm) {
                serializableStateInstances.Add(SerializeStateInstance(kvp));
            }

            foreach (KeyValuePair<string, FsmParameter> keyValuePair in parameters) {
                serializableFsmParameters.Add(keyValuePair.Value.Serialize(keyValuePair.Key));
            }
        }

        private static SerializableStateInstance SerializeStateInstance (KeyValuePair<string, StateInstance> kvp) {
            SerializableStateInstance serializableStateInstance = new SerializableStateInstance() {
                name = kvp.Key,
                state = kvp.Value.state,
                transitions = new List<SerializableFsmTransition>()
            };

            foreach (TransitionInstance transitionInstance in kvp.Value.transitions) {
                SerializableFsmTransition serializableTransition = SerializeTransitionInstance(transitionInstance);
                serializableStateInstance.transitions.Add(serializableTransition);
            }

            return serializableStateInstance;
        }

        private static SerializableFsmTransition SerializeTransitionInstance (TransitionInstance transitionInstance) {
            SerializableFsmTransition serializableTransition = new SerializableFsmTransition() {
                serializableFsmConditions = new List<SerializableFsmCondition>(),
                targetState = transitionInstance.targetState
            };

            foreach (FsmCondition condition in transitionInstance.transition) {
                serializableTransition.serializableFsmConditions.Add(condition.Serialize());
            }

            return serializableTransition;
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
                if (serializableStateInstance.name.Length == 0) {
                    serializableStateInstance.name = "New state";
                }

                // Prevent duplicate names (not permitted by dictionary)
                while (fsm.ContainsKey(serializableStateInstance.name)) {
                    serializableStateInstance.name = IncrementName(serializableStateInstance.name);
                }

                fsm.Add(serializableStateInstance.name, DeserializeStateInstance(serializableStateInstance));
            }
        }

        private StateInstance DeserializeStateInstance (SerializableStateInstance serializableStateInstance) {
            StateInstance stateInstance = new StateInstance() {
                state = serializableStateInstance.state,
                transitions = new List<TransitionInstance>()
            };

            foreach (SerializableFsmTransition serializableFsmTransition in serializableStateInstance.transitions) {
                stateInstance.transitions.Add(DeserializeTransitionInstance(serializableFsmTransition));
            }

            return stateInstance;
        }

        private TransitionInstance DeserializeTransitionInstance (SerializableFsmTransition serializableFsmTransition) {
            TransitionInstance transitionInstance = new TransitionInstance() {
                targetState = serializableFsmTransition.targetState,
                transition = new FsmTransition()
            };

            foreach (SerializableFsmCondition serializableFsmCondition in serializableFsmTransition.serializableFsmConditions) {
                transitionInstance.transition.AddCondition(serializableFsmCondition.Deserialize(parameters));
            }

            return transitionInstance;
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
