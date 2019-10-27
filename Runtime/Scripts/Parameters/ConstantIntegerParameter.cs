using Helluys.FsmCore.Serialization;

namespace Helluys.FsmCore.Parameters
{
    public class ConstantIntegerParameter : FsmConstant {
        public int value;

        public override SerializedFsmConstant Serialize () {
            return new SerializedFsmConstant() {
                type = SerializedFsmConstant.Type.INTEGER,
                integerValue = value
            };
        }
    }
}