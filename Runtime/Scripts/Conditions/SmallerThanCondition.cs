namespace fsm {
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

        public override SerializableFsmCondition Serialize () {
            return new SerializableFsmCondition() {
                type = SerializableFsmCondition.Type.SMALLER_THAN,
            };
        }
    }
}
