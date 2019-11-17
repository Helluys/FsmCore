using NUnit.Framework;

namespace Helluys.FsmCore.Tests.Parameters {
	[TestFixture]
	public class IntegerParameterTests {
		[Test]
		public void EqualsTest() {
			FsmParameter int1 = FsmParameter.NewInteger("int1", 1);

			Assert.IsTrue(int1.Equals(1));
			Assert.IsFalse(int1.Equals(2));

			int1.intValue = 2;

			Assert.IsFalse(int1.Equals(1));
			Assert.IsTrue(int1.Equals(2));
		}

		[Test]
		public void GreaterThanTest() {
			FsmParameter int1 = FsmParameter.NewInteger("int1", 1);

			Assert.IsTrue(int1.GreaterThan(0));
			Assert.IsFalse(int1.GreaterThan(1));
			Assert.IsFalse(int1.GreaterThan(2));
		}

		[Test]
		public void SmallerThanTest() {
			FsmParameter int1 = FsmParameter.NewInteger("int1", 1);

			Assert.IsFalse(int1.SmallerThan(0));
			Assert.IsFalse(int1.SmallerThan(1));
			Assert.IsTrue(int1.SmallerThan(2));
		}
	}
}