using NUnit.Framework;

namespace Helluys.FsmCore.Tests.Conditions {
	[TestFixture]
	public class EqualsConditionTests {
		[Test]
		public void BooleanEvaluationTest() {
			FsmParameter bool1 = FsmParameter.NewBoolean("bool1");
			FsmCondition condition = FsmCondition.NewEquals(bool1, false);

			bool1.boolValue = false;
			condition.boolValue = false;
			Assert.IsTrue(condition.Evaluate());

			bool1.boolValue = false;
			condition.boolValue = true;
			Assert.IsFalse(condition.Evaluate());

			bool1.boolValue = true;
			condition.boolValue = false;
			Assert.IsFalse(condition.Evaluate());

			bool1.boolValue = true;
			condition.boolValue = true;
			Assert.IsTrue(condition.Evaluate());
		}

		[Test]
		public void IntegerEvaluationTest() {
			FsmParameter int1 = FsmParameter.NewInteger("int1");
			FsmCondition condition = FsmCondition.NewEquals(int1, 0);

			int1.intValue = 0;
			condition.intValue = 0;
			Assert.IsTrue(condition.Evaluate());

			int1.intValue = 0;
			condition.intValue = 1;
			Assert.IsFalse(condition.Evaluate());

			int1.intValue = 1;
			condition.intValue = 0;
			Assert.IsFalse(condition.Evaluate());

			int1.intValue = 1;
			condition.intValue = 1;
			Assert.IsTrue(condition.Evaluate());
		}

		[Test]
		public void FloatEvaluationTest() {
			FsmParameter float1 = FsmParameter.NewFloat("float1");
			FsmCondition condition = FsmCondition.NewEquals(float1, 1f);

			float1.floatValue = 0f;
			condition.floatValue = 0f;
			Assert.IsTrue(condition.Evaluate());

			float1.floatValue = 0f;
			condition.floatValue = 1f;
			Assert.IsFalse(condition.Evaluate());

			float1.floatValue = 1f;
			condition.floatValue = 0f;
			Assert.IsFalse(condition.Evaluate());

			float1.floatValue = 1f;
			condition.floatValue = 1f;
			Assert.IsTrue(condition.Evaluate());
		}
	}
}
