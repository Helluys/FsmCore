using System;
using Helluys.FsmCore.Serialization;

namespace Helluys.FsmCore.Parameters {
    public class BooleanParameter : FsmParameter {
        public bool value;

        public bool Get () {
            return value;
        }

        public void Set (bool newValue) {
            value = newValue;
        }

        public override bool Equals (object other) {
            if (other == this) {
                return true;
            } else if (other is BooleanParameter) {
                return (other as BooleanParameter).value == value;
            } else {
                return false;
            }
        }

        public override int GetHashCode () {
            return value.GetHashCode();
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
                type = SerializableFsmParameter.Type.BOOLEAN,
                defaultValue = new ConstantBooleanParameter() {
                    value = value
                }.Serialize()
            };
        }
    }
}
