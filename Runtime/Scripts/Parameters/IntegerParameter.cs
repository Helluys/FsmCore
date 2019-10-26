using System;

namespace fsm {
    public class IntegerParameter : FsmParameter {
        public int value;

        public int Get () {
            return value;
        }

        public void Set (int newValue) {
            value = newValue;
        }

        public override bool Equals (FsmConstant constant) {
            if (constant is ConstantIntegerParameter) {
                return value == (constant as ConstantIntegerParameter).value;
            } else {
                throw new NotSupportedException();
            }
        }

        public override bool GreaterThan (FsmConstant constant) {
            if (constant is ConstantIntegerParameter) {
                return value > (constant as ConstantIntegerParameter).value;
            } else {
                throw new NotSupportedException();
            }
        }

        public override bool SmallerThan (FsmConstant constant) {
            if (constant is ConstantIntegerParameter) {
                return value < (constant as ConstantIntegerParameter).value;
            } else {
                throw new NotSupportedException();
            }
        }

        public override SerializableFsmParameter Serialize (string name) {
            return new SerializableFsmParameter() {
                name = name,
                type = SerializableFsmParameter.Type.INTEGER
            };
        }
    }
}
