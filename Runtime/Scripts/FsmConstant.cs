using Helluys.FsmCore.Serialization;

namespace Helluys.FsmCore {
    public abstract class FsmConstant {
        public abstract SerializableFsmConstant Serialize ();
    }
}