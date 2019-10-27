using System;
using System.Collections.Generic;

namespace Helluys.FsmCore.Serialization
{
    [Serializable]
    public struct SerializedFsmTransition
    {
        public List<SerializedFsmCondition> serializableFsmConditions;

        public static SerializedFsmTransition Serialize (FsmTransition transition) {
            List<SerializedFsmCondition> conditions = new List<SerializedFsmCondition>();
            foreach (FsmCondition condition in transition) {
                conditions.Add(condition.Serialize());
            }

            return new SerializedFsmTransition() {
                serializableFsmConditions = conditions
            };
        }

        public FsmTransition Deserialize (IDictionary<string, FsmParameter> parameters) {
            FsmTransition transition = new FsmTransition();
            foreach (SerializedFsmCondition serializableFsmCondition in serializableFsmConditions) {
                transition.AddCondition(serializableFsmCondition.Deserialize(parameters));
            }

            return transition;
        }
    }
}
