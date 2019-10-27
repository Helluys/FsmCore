using Helluys.FsmCore.Serialization;

namespace Helluys.FsmCore.Conditions
{
    public class SmallerThanCondition : FsmCondition {
        private FsmParameter parameter;
        private FsmConstant constant;

        public SmallerThanCondition (FsmParameter parameter, FsmConstant constant) {
            this.parameter = parameter;
            this.constant = constant;
        }

        public override bool Contains (FsmParameter parameter) {
            return this.parameter == parameter;
        }

        public override bool Evaluate () {
            return parameter.SmallerThan(constant);
        }

        public override SerializedFsmCondition Serialize () {
            return new SerializedFsmCondition() {
                type = SerializedFsmCondition.Type.SMALLER_THAN,
                parameterName = parameter.name,
                constant = constant.Serialize()
            };
        }
    }
}
