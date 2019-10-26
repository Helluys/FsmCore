using System;
using System.Collections.Generic;

namespace Helluys.FsmCore.Serialization
{
    [Serializable]
    public struct SerializableFsmTransition
    {
        public List<SerializableFsmCondition> serializableFsmConditions;

        public static SerializableFsmTransition Serialize (FsmTransition transition) {
            List<SerializableFsmCondition> conditions = new List<SerializableFsmCondition>();
            foreach (FsmCondition condition in transition) {
                conditions.Add(condition.Serialize());
            }

            return new SerializableFsmTransition() {
                serializableFsmConditions = conditions
            };
        }

        public FsmTransition Deserialize (IDictionary<string, FsmParameter> parameters) {
            FsmTransition transition = new FsmTransition();
            foreach (SerializableFsmCondition serializableFsmCondition in serializableFsmConditions) {
                transition.AddCondition(serializableFsmCondition.Deserialize(parameters));
            }

            return transition;
        }
    }
}
