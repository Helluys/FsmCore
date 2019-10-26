using System;
using Helluys.FsmCore.Serialization;

namespace Helluys.FsmCore.Parameters
{
    public class FloatParameter : FsmParameter {
        public float value;

        public float Get () {
            return value;
        }

        public void Set (float newValue) {
            value = newValue;
        }

        public override bool Equals (object other) {
            if (other == this) {
                return true;
            } else if (other is FloatParameter) {
                return Math.Abs((other as FloatParameter).value - value) < float.Epsilon;
            } else {
                return false;
            }
        }

        public override int GetHashCode () {
            return value.GetHashCode();
        }

        public override bool Equals (FsmConstant constant) {
            if (constant is ConstantFloatParameter) {
                return Math.Abs((constant as ConstantFloatParameter).value - value) < float.Epsilon;
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
                type = SerializableFsmParameter.Type.FLOAT,
                defaultValue = new ConstantFloatParameter() {
                    value = value
                }.Serialize()
            };
        }
    }
}
