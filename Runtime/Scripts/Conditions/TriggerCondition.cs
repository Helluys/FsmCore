using Helluys.FsmCore.Parameters;
using Helluys.FsmCore.Serialization;

namespace Helluys.FsmCore.Conditions
{
    public class TriggerCondition : FsmCondition
    {
        private TriggerParameter trigger;

        public TriggerCondition (TriggerParameter trigger) {
            this.trigger = trigger;
        }

        public override bool Contains (FsmParameter parameter) {
            return trigger == parameter;
        }

        public override bool Evaluate () {
            return trigger.Get();
        }

        public override SerializableFsmCondition Serialize () {
            // Backup trigger value
            bool triggerValue = (trigger != null) ? trigger.Get() : false;

            // Serialize
            SerializableFsmCondition serializableFsmCondition = new SerializableFsmCondition() {
                type = SerializableFsmCondition.Type.TRIGGER,
                parameterName = trigger?.name,
                constant = new ConstantBooleanParameter() { value = triggerValue }.Serialize()
            };

            // Restore trigger value if consumed
            if (triggerValue) {
                trigger.Set();
            }

            return serializableFsmCondition;
        }
    }
}