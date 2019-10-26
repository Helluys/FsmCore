using System;

namespace fsm {
    public class FloatParameter : FsmParameter {
        public float value;

        public float Get () {
            return value;
        }

        public void Set (float newValue) {
            value = newValue;
        }

        public override bool Equals (FsmConstant constant) {
            if (constant is ConstantFloatParameter) {
                return value == (constant as ConstantFloatParameter).value;
            } else {
                throw new NotSupportedException();
            }
        }

        public override bool GreaterThan (FsmConstant constant) {
            if (constant is ConstantFloatParameter) {
                return value > (constant as ConstantFloatParameter).value;
            } else {
                throw new NotSupportedException();
            }
        }

        public override bool SmallerThan (FsmConstant constant) {
            if (constant is ConstantFloatParameter) {
                return value < (constant as ConstantFloatParameter).value;
            } else {
                throw new NotSupportedException();
            }
        }

        public override SerializableFsmParameter Serialize (string name) {
            return new SerializableFsmParameter() {
                name = name,
                type = SerializableFsmParameter.Type.FLOAT
            };
        }
    }
}
