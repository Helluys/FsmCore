using System;

namespace fsm {
    public class BooleanParameter : FsmParameter {
        public bool value;

        public bool Get () {
            return value;
        }

        public void Set (bool newValue) {
            value = newValue;
        }

        public override bool Equals (FsmConstant constant) {
            if (constant is ConstantBooleanParameter) {
                return (constant as ConstantBooleanParameter).value == value;
            } else {
                throw new NotSupportedException();
            }
        }

        public override bool GreaterThan (FsmConstant constant) {
            throw new NotSupportedException();
        }

        public override bool SmallerThan (FsmConstant constant) {
            throw new NotSupportedException();
        }

        public override SerializableFsmParameter Serialize (string name) {
            return new SerializableFsmParameter() {
                name = name,
                type = SerializableFsmParameter.Type.BOOLEAN
            };
        }
    }
}
