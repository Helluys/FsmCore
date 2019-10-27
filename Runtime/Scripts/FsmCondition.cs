using Helluys.FsmCore.Serialization;

namespace Helluys.FsmCore {
    public abstract class FsmCondition {
        public const string DEFAULT_PARAMETER_NAME = "Parameter";

        public abstract bool Contains (FsmParameter parameter);
        public abstract bool Evaluate ();
        public abstract SerializedFsmCondition Serialize ();
    }
}
