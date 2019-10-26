using Helluys.FsmCore.Serialization;

namespace Helluys.FsmCore.Parameters
{
    public class ConstantIntegerParameter : FsmConstant {
        public int value;

        public override SerializableFsmConstant Serialize () {
            return new SerializableFsmConstant() {
                type = SerializableFsmConstant.Type.INTEGER,
                integerValue = value
            };
        }
    }
}