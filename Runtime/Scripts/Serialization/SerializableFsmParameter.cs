using System;
using System.ComponentModel;

namespace fsm {
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

        public FsmParameter Deserialize () {
            switch (type) {
                case Type.TRIGGER:
                    return new TriggerParameter();
                case Type.BOOLEAN:
                    return new BooleanParameter();
                case Type.INTEGER:
                    return new IntegerParameter();
                case Type.FLOAT:
                    return new FloatParameter();
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
