using Helluys.FsmCore.Serialization;

namespace Helluys.FsmCore.Parameters
{
    public class ConstantBooleanParameter : FsmConstant {
        public bool value;

        public override SerializedFsmConstant Serialize () {
            return new SerializedFsmConstant() {
                type = SerializedFsmConstant.Type.BOOLEAN,
                booleanValue = value
            };
        }
    }
}
