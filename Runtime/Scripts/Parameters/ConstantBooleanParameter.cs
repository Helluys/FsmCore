namespace fsm {
    public class ConstantBooleanParameter : FsmConstant {
        public bool value;

        public override SerializableFsmConstant Serialize () {
            return new SerializableFsmConstant() {
                type = SerializableFsmConstant.Type.BOOLEAN,
                booleanValue = value
            };
        }
    }
}
