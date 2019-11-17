using NUnit.Framework;

namespace Helluys.FsmCore.Tests.Conditions {
	[TestFixture]
	public class ContainsTests {

		[Test]
		public void BooleanContainsTest() {
			FsmParameter bool1 = FsmParameter.NewBoolean("bool1");
			FsmParameter bool2 = FsmParameter.NewBoolean("bool2");
			FsmCondition condition = FsmCondition.NewEquals(bool1, false);

			Assert.IsTrue(condition.Contains("bool1"));
			Assert.IsFalse(condition.Contains("bool2"));
		}

		[Test]
		public void IntegerContainsTest() {
			FsmParameter int1 = FsmParameter.NewInteger("int1");
			FsmParameter int2 = FsmParameter.NewInteger("int2");
			FsmCondition condition = FsmCondition.NewEquals(int1, 0);

			Assert.IsTrue(condition.Contains("int1"));
			Assert.IsFalse(condition.Contains("int2"));
		}

		[Test]
		public void FloatContainsTest() {
			FsmParameter float1 = FsmParameter.NewFloat("float1");
			FsmParameter float2 = FsmParameter.NewFloat("float2");
			FsmCondition condition = FsmCondition.NewEquals(float1, 0f);

			Assert.IsTrue(condition.Contains("float1"));
			Assert.IsFalse(condition.Contains("float2"));
		}
	}
}
