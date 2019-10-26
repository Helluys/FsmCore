namespace fsm {
    public abstract class FsmParameter {
        public string name { get; } = "New parameter";
        public abstract bool Equals (FsmConstant constant);
        public abstract bool GreaterThan (FsmConstant constant);
        public abstract bool SmallerThan (FsmConstant constant);
        public abstract SerializableFsmParameter Serialize (string name);
    }
}
