using UnityEngine;

namespace fsm {
    public class FsmState : ScriptableObject {

        public virtual void OnEnter () { }
        public virtual void OnStay () { }
        public virtual void OnExit () { }

    }
}