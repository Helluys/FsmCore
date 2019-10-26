﻿using Helluys.FsmCore.Serialization;

namespace Helluys.FsmCore {
    public abstract class FsmCondition {
        public abstract bool Contains (FsmParameter parameter);
        public abstract bool Evaluate ();
        public abstract SerializableFsmCondition Serialize ();
    }
}
