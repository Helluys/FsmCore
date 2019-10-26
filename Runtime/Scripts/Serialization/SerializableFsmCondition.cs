using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace fsm {
    [Serializable]
    public class SerializableFsmCondition {
        public enum Type {
            EQUALS,
            GREATER_THAN,
            SMALLER_THAN,
            TRIGGER
        }

        public Type type;
        public string parameterName;
        public SerializableFsmConstant constant;

        public FsmCondition Deserialize (IDictionary<string, FsmParameter> parameters) {
            parameters.TryGetValue(parameterName, out FsmParameter parameter);

            switch (type) {
                case Type.EQUALS:
                    return new EqualsCondition(parameter, constant.Deserialize());
                case Type.GREATER_THAN:
                    return new GreaterThanCondition(parameter, constant.Deserialize());
                case Type.SMALLER_THAN:
                    return new SmallerThanCondition(parameter, constant.Deserialize());
                case Type.TRIGGER:
                    return new TriggerCondition(parameter as TriggerParameter);
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
