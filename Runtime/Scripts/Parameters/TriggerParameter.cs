using System;
using Helluys.FsmCore.Serialization;

namespace Helluys.FsmCore.Parameters
{
    public class TriggerParameter : FsmParameter {
        private bool value;

        public bool Get () {
            bool result = value;
            value = false;
            return result;
        }

        public void Set () {
            value = true;
        }

        public override bool Equals (FsmConstant constant) {
            throw new NotSupportedException();
        }

        public override bool GreaterThan (FsmConstant constant) {
            throw new NotSupportedException();
        }

        public override bool SmallerThan (FsmConstant constant) {
            throw new NotSupportedException();
        }

        public override SerializedFsmParameter Serialize (string name) {
            return new SerializedFsmParameter() {
                name = name,
                type = SerializedFsmParameter.Type.TRIGGER,
            };
        }
    }
}
