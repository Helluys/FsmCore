using Helluys.FsmCore.Serialization;

namespace Helluys.FsmCore
{
    public abstract class FsmParameter
    {
        public string name { get; set; } = "New parameter";
        public abstract bool Equals (FsmConstant constant);
        public abstract bool GreaterThan (FsmConstant constant);
        public abstract bool SmallerThan (FsmConstant constant);
        public abstract SerializedFsmParameter Serialize (string name);
    }
}
