using System;
using System.Collections.Generic;
using System.ComponentModel;
using Helluys.FsmCore.Conditions;
using Helluys.FsmCore.Parameters;

namespace Helluys.FsmCore.Serialization
{
    [Serializable]
    public struct SerializedFsmCondition
    {
        public enum Type
        {
            EQUALS,
            GREATER_THAN,
            SMALLER_THAN,
            TRIGGER
        }

        public Type type;
        public string parameterName;
        public SerializedFsmConstant constant;

        public static SerializedFsmCondition Serialize (FsmCondition condition) {
            return condition.Serialize();
        }

        public FsmCondition Deserialize (IDictionary<string, FsmParameter> parameters) {
            FsmParameter parameter;
            if (parameters.ContainsKey(parameterName)) {
                parameter = parameters[parameterName];
            } else {
                parameter = new BooleanParameter();
            }

            SerializedFsmConstant.Type constantType = ExpectedConstantType(parameter);
            if (constantType != constant.type) {
                ReplaceConstant(constantType);
            }

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
                    throw new InvalidEnumArgumentException(type.ToString());
            }
        }

        private SerializedFsmConstant.Type ExpectedConstantType (FsmParameter parameter) {
            if (parameter is BooleanParameter || parameter is TriggerParameter) {
                return SerializedFsmConstant.Type.BOOLEAN;
            } else if (parameter is IntegerParameter) {
                return SerializedFsmConstant.Type.INTEGER;
            } else if (parameter is FloatParameter) {
                return SerializedFsmConstant.Type.FLOAT;
            } else {
                throw new InvalidOperationException("Unknown parameter type " + parameter.GetType());
            }
        }

        private void ReplaceConstant (SerializedFsmConstant.Type constantType) {
            switch (constantType) {
                case SerializedFsmConstant.Type.BOOLEAN:
                    constant = new ConstantBooleanParameter().Serialize();
                    break;
                case SerializedFsmConstant.Type.INTEGER:
                    constant = new ConstantIntegerParameter().Serialize();
                    break;
                case SerializedFsmConstant.Type.FLOAT:
                    constant = new ConstantFloatParameter().Serialize();
                    break;
                default:
                    throw new InvalidEnumArgumentException(type.ToString());
            }
        }
    }
}
