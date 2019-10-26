using System;
using System.Collections.Generic;
using System.ComponentModel;
using Helluys.FsmCore.Conditions;
using Helluys.FsmCore.Parameters;
using UnityEngine;

namespace Helluys.FsmCore.Serialization {
    [Serializable]
    public struct SerializableFsmCondition {
        public enum Type {
            EQUALS,
            GREATER_THAN,
            SMALLER_THAN,
            TRIGGER
        }

        public Type type;
        public string parameterName;
        public SerializableFsmConstant constant;

        public static SerializableFsmCondition Serialize (FsmCondition condition) {
            return condition.Serialize();
        }

        public FsmCondition Deserialize (IDictionary<string, FsmParameter> parameters) {
            FsmParameter parameter = parameters[parameterName];

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
