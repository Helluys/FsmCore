namespace fsm {
    public class ConstantFloatParameter : FsmConstant {
        public float value;

        public override SerializableFsmConstant Serialize () {
            return new SerializableFsmConstant() {
                type = SerializableFsmConstant.Type.FLOAT,
                floatValue = value
            };
        }
    }
}