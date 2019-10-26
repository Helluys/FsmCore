using UnityEngine;
using System.Collections;

namespace fsm {
    public class TriggerCondition : FsmCondition {
        private TriggerParameter trigger;

        public TriggerCondition(TriggerParameter trigger) {
            this.trigger = trigger;
        }

        public override bool Contains (FsmParameter parameter) {
            return trigger == parameter;
        }

        public override bool Evaluate () {
            return trigger.Get();
        }

        public override SerializableFsmCondition Serialize () {
            return new SerializableFsmCondition() {
                type = SerializableFsmCondition.Type.TRIGGER,
                parameterName = trigger?.name
            };
        }
    }
}