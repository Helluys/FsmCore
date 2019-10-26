using System;
using System.ComponentModel;
using Helluys.FsmCore.Parameters;

namespace Helluys.FsmCore.Serialization
{
    [Serializable]
    public class SerializableFsmParameter
    {
        public enum Type
        {
            TRIGGER,
            BOOLEAN,
            INTEGER,
            FLOAT
        }

        public string name;
        public Type type;
        public SerializableFsmConstant defaultValue;

        public static SerializableFsmParameter Serialize (string name, FsmParameter fsmParameter) {
            return fsmParameter.Serialize(name);
        }

        public FsmParameter Deserialize () {
            FsmParameter result;
            FsmConstant fsmConstant = defaultValue?.Deserialize();

            switch (type) {

                case Type.TRIGGER:
                    result = new TriggerParameter() {
                        name = name,
                    };
                    break;

                case Type.BOOLEAN:
                    result = new BooleanParameter() {
                        name = name,
                        value = (fsmConstant is ConstantBooleanParameter) ? (fsmConstant as ConstantBooleanParameter).value : false
                    };
                    break;

                case Type.INTEGER:
                    result = new IntegerParameter() {
                        name = name,
                        value = (fsmConstant is ConstantIntegerParameter) ? (fsmConstant as ConstantIntegerParameter).value : 0
                    };
                    break;

                case Type.FLOAT:
                    result = new FloatParameter() {
                        name = name,
                        value = (fsmConstant is ConstantFloatParameter) ? (fsmConstant as ConstantFloatParameter).value : 0f
                    };
                    break;

                default:
                    throw new InvalidEnumArgumentException();

            };

            return result;
        }
    }
}
