using Helluys.FsmCore.Parameters;
using NUnit.Framework;

namespace Helluys.FsmCore.Tests.Parameters
{
    [TestFixture]
    public class IntegerParameterTests
    {
        [Test]
        public void EqualsTest () {
            IntegerParameter int1 = new IntegerParameter() {
                name = "int1",
                value = 1
            };

            IntegerParameter int2 = new IntegerParameter() {
                name = "int2",
                value = 2
            };

            ConstantIntegerParameter const1 = new ConstantIntegerParameter() {
                value = 1
            };

            ConstantIntegerParameter const2 = new ConstantIntegerParameter() {
                value = 2
            };

            Assert.IsTrue(int1.Equals(const1));
            Assert.IsFalse(int1.Equals(const2));

            Assert.IsFalse(int2.Equals(const1));
            Assert.IsTrue(int2.Equals(const2));

            Assert.IsTrue(int1.Equals(int1));
            Assert.IsTrue(int2.Equals(int2));
            Assert.IsFalse(int1.Equals(int2));

            int1.value = 2;

            Assert.IsFalse(int1.Equals(const1));
            Assert.IsTrue(int1.Equals(const2));

            Assert.IsTrue(int1.Equals(int2));

            int2.value = 3;

            Assert.IsFalse(int2.Equals(const1));
            Assert.IsFalse(int2.Equals(const2));

            Assert.IsTrue(int2.Equals(int2));

            Assert.IsFalse(int1.Equals(int2));
        }

        [Test]
        public void GreaterThanTest () {

            IntegerParameter int1 = new IntegerParameter() {
                name = "int1",
                value = 1
            };

            ConstantIntegerParameter const0 = new ConstantIntegerParameter() {
                value = 0
            };
            ConstantIntegerParameter const1 = new ConstantIntegerParameter() {
                value = 1
            };
            ConstantIntegerParameter const2 = new ConstantIntegerParameter() {
                value = 2
            };

            Assert.IsTrue(int1.GreaterThan(const0));
            Assert.IsFalse(int1.GreaterThan(const1));
            Assert.IsFalse(int1.GreaterThan(const2));
        }

        [Test]
        public void SmallerThanTest () {

            IntegerParameter int1 = new IntegerParameter() {
                name = "int1",
                value = 1
            };


            ConstantIntegerParameter const0 = new ConstantIntegerParameter() {
                value = 0
            };
            ConstantIntegerParameter const1 = new ConstantIntegerParameter() {
                value = 1
            };
            ConstantIntegerParameter const2 = new ConstantIntegerParameter() {
                value = 2
            };

            Assert.IsFalse(int1.SmallerThan(const0));
            Assert.IsFalse(int1.SmallerThan(const1));
            Assert.IsTrue(int1.SmallerThan(const2));
        }
    }
}