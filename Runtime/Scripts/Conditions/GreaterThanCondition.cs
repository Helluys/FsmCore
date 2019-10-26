using Helluys.FsmCore.Serialization;

namespace Helluys.FsmCore.Conditions
{
    public class GreaterThanCondition : FsmCondition {
        private FsmParameter parameter;
        private FsmConstant constant;

        public GreaterThanCondition (FsmParameter parameter, FsmConstant constant) {
            this.parameter = parameter;
            this.constant = constant;
        }

        public override bool Contains (FsmParameter parameter) {
            return this.parameter == parameter;
        }

        public override bool Evaluate () {
            return parameter.GreaterThan(constant);
        }

        public override SerializableFsmCondition Serialize () {
            return new SerializableFsmCondition() {
                type = SerializableFsmCondition.Type.GREATER_THAN,
                parameterName = null,
                constant = null
            };
        }
    }
}
