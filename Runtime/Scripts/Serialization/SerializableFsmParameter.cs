using System;
using System.ComponentModel;
using Helluys.FsmCore.Parameters;

namespace Helluys.FsmCore.Serialization {
    [Serializable]
    public class SerializableFsmParameter {
        public enum Type {
            TRIGGER,
            BOOLEAN,
            INTEGER,
            FLOAT
        }

        public string name;
        public Type type;
        public SerializableFsmConstant defaultValue;

        public FsmParameter Deserialize () {
            FsmParameter result;
            switch (type) {
                case Type.TRIGGER:
                    result = new TriggerParameter() {
                        name = name
                    };
                    break;
                case Type.BOOLEAN:
                    result = new BooleanParameter() {
                        name = name,
                        value = (defaultValue.Deserialize() as ConstantBooleanParameter).value
                    };
                    break;
                case Type.INTEGER:
                    result = new IntegerParameter() {
                        name = name,
                        value = (defaultValue.Deserialize() as ConstantIntegerParameter).value
                    };
                    break;
                case Type.FLOAT:
                    result = new FloatParameter() {
                        name = name,
                        value = (defaultValue.Deserialize() as ConstantFloatParameter).value
                    };
                    break;
                default:
                    throw new InvalidEnumArgumentException();
            };

            return result;
        }
    }
}
