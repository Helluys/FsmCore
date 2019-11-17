using NUnit.Framework;

namespace Helluys.FsmCore.Tests.Parameters {
	[TestFixture]
	public class FloatParameterTests {
		[Test]
		public void EqualsTest() {
			FsmParameter float1 = FsmParameter.NewFloat("float1", 1f);

			Assert.IsFalse(float1.Equals(0f));
			Assert.IsTrue(float1.Equals(1f));
			Assert.IsFalse(float1.Equals(2f));

			float1.floatValue = 2f;

			Assert.IsFalse(float1.Equals(0f));
			Assert.IsFalse(float1.Equals(1f));
			Assert.IsTrue(float1.Equals(2f));
		}

		[Test]
		public void GreaterThanTest() {
			FsmParameter float1 = FsmParameter.NewFloat("float1", 1f);

			Assert.IsTrue(float1.GreaterThan(0f));
			Assert.IsFalse(float1.GreaterThan(1f));
			Assert.IsFalse(float1.GreaterThan(2f));
		}

		[Test]
		public void SmallerThanTest() {
			FsmParameter float1 = FsmParameter.NewFloat("float1", 1f);

			Assert.IsFalse(float1.SmallerThan(0f));
			Assert.IsFalse(float1.SmallerThan(1f));
			Assert.IsTrue(float1.SmallerThan(2f));
		}
	}
}