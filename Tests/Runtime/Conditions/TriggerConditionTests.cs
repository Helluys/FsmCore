using NUnit.Framework;
using System;

namespace Helluys.FsmCore.Tests.Conditions {
	[TestFixture]
	public class TriggerConditionTests {
		[Test]
		public void BooleanEvaluationTest() {
			FsmParameter t1 = FsmParameter.NewBoolean("t1");
			FsmCondition tc = FsmCondition.NewTrigger(t1);
			Assert.IsFalse(tc.Evaluate());

			t1.boolValue = true;

			Assert.IsTrue(tc.Evaluate());
			Assert.IsFalse(tc.Evaluate());
		}
	}
}
