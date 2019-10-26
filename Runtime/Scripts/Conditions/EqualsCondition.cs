using Helluys.FsmCore.Serialization;

namespace Helluys.FsmCore.Conditions
{
    public class EqualsCondition : FsmCondition {
        private FsmParameter parameter;
        private FsmConstant constant;

        public EqualsCondition (FsmParameter parameter, FsmConstant constant) {
            this.parameter = parameter;
            this.constant = constant;
        }

        public override bool Contains (FsmParameter parameter) {
            return this.parameter == parameter;
        }

        public override bool Evaluate () {
            return parameter.Equals(constant);
        }

        public override SerializableFsmCondition Serialize() {
            return new SerializableFsmCondition() {
                type = SerializableFsmCondition.Type.EQUALS,
                parameterName = parameter?.name,
                constant = constant.Serialize()
            };
        }
    }
}