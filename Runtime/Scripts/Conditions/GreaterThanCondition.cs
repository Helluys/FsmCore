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

        public override SerializedFsmCondition Serialize () {
            return new SerializedFsmCondition() {
                type = SerializedFsmCondition.Type.GREATER_THAN,
                parameterName = parameter.name,
                constant = constant.Serialize()
            };
        }
    }
}
