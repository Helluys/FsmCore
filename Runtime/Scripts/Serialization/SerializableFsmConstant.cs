using System;
using System.ComponentModel;
using Helluys.FsmCore.Parameters;

namespace Helluys.FsmCore.Serialization {
    [Serializable]
    public class SerializableFsmConstant {
        public enum Type {
            BOOLEAN,
            INTEGER,
            FLOAT
        }

        public Type type;
        public bool booleanValue;
        public int integerValue;
        public float floatValue;

        public FsmConstant Deserialize () {
            switch (type) {
                case Type.BOOLEAN:
                    return new ConstantBooleanParameter() {
                        value = booleanValue
                    };
                case Type.INTEGER:
                    return new ConstantIntegerParameter() {
                        value = integerValue
                    };
                case Type.FLOAT:
                    return new ConstantFloatParameter() {
                        value = floatValue
                    };
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
