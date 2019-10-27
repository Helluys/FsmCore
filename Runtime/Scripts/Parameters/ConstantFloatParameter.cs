using Helluys.FsmCore.Serialization;

namespace Helluys.FsmCore.Parameters
{
    public class ConstantFloatParameter : FsmConstant {
        public float value;

        public override SerializedFsmConstant Serialize () {
            return new SerializedFsmConstant() {
                type = SerializedFsmConstant.Type.FLOAT,
                floatValue = value
            };
        }
    }
}