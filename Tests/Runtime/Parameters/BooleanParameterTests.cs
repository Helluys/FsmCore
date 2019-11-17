using System;
using NUnit.Framework;

namespace Helluys.FsmCore.Tests.Parameters {
	[TestFixture]
	public class BooleanParameterTests {
		
		[Test]
		public void EqualsTest() {
			FsmParameter bool1 = FsmParameter.NewBoolean("bool1");

			Assert.IsTrue(bool1.Equals(false));
			Assert.IsFalse(bool1.Equals(true));

			bool1.boolValue = true;

			Assert.IsFalse(bool1.Equals(false));
			Assert.IsTrue(bool1.Equals(true));
		}

		[Test]
		public void GreaterThanTest() {
			FsmParameter bool1 = FsmParameter.NewBoolean("bool1");
			Assert.Throws<NotSupportedException>(() => bool1.GreaterThan(1));
			Assert.Throws<NotSupportedException>(() => bool1.GreaterThan(1f));
		}

		[Test]
		public void SmallerThanTest() {
			FsmParameter bool1 = FsmParameter.NewBoolean("bool1");
			Assert.Throws<NotSupportedException>(() => bool1.SmallerThan(1));
			Assert.Throws<NotSupportedException>(() => bool1.SmallerThan(1f));
		}
	}
}