using Helluys.FsmCore.Serialization;

namespace Helluys.FsmCore.Parameters
{
    public class ConstantFloatParameter : FsmConstant {
        public float value;

        public override SerializableFsmConstant Serialize () {
            return new SerializableFsmConstant() {
                type = SerializableFsmConstant.Type.FLOAT,
                floatValue = value
            };
        }
    }
}