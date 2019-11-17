using System;
using NUnit.Framework;

namespace Helluys.FsmCore.Tests.Conditions {
	[TestFixture]
	public class GreaterThanConditionTests {
		[Test]
		public void BooleanEvaluationTest() {
			FsmParameter bool1 = FsmParameter.NewBoolean("bool1");
			FsmCondition condition = FsmCondition.NewGreaterThan(bool1, 0f);

			Assert.Throws<NotSupportedException>(() => condition.Evaluate());
		}

		[Test]
		public void IntegerEvaluationTest() {
			FsmParameter int1 = FsmParameter.NewInteger("int1");
			FsmCondition condition = FsmCondition.NewGreaterThan(int1, 0);

			int1.intValue = 0;
			condition.intValue = 0;
			Assert.IsFalse(condition.Evaluate());

			int1.intValue = 0;
			condition.intValue = 1;
			Assert.IsFalse(condition.Evaluate());

			int1.intValue = 1;
			condition.intValue = 0;
			Assert.IsTrue(condition.Evaluate());
		}

		[Test]
		public void FloatEvaluationTest() {
			FsmParameter float1 = FsmParameter.NewFloat("float1");
			FsmCondition condition = FsmCondition.NewGreaterThan(float1, 0f);

			float1.floatValue = 0;
			condition.floatValue = 0;
			Assert.IsFalse(condition.Evaluate());

			float1.floatValue = 0;
			condition.floatValue = 1;
			Assert.IsFalse(condition.Evaluate());

			float1.floatValue = 1;
			condition.floatValue = 0;
			Assert.IsTrue(condition.Evaluate());
		}
	}
}
