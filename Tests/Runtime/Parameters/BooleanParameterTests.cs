using System;
using Helluys.FsmCore.Parameters;
using NUnit.Framework;

namespace Helluys.FsmCore.Tests.Parameters
{
    [TestFixture]
    public class BooleanParameterTests
    {
        [Test]
        public void EqualsTest () {
            BooleanParameter bool1 = new BooleanParameter() {
                name = "bool1",
                value = false
            };

            BooleanParameter bool2 = new BooleanParameter() {
                name = "bool2",
                value = true
            };

            ConstantBooleanParameter falseConstant = new ConstantBooleanParameter() {
                value = false
            };

            ConstantBooleanParameter trueConstant = new ConstantBooleanParameter() {
                value = true
            };

            Assert.IsTrue(bool1.Equals(falseConstant));
            Assert.IsFalse(bool1.Equals(trueConstant));

            Assert.IsFalse(bool2.Equals(falseConstant));
            Assert.IsTrue(bool2.Equals(trueConstant));

            Assert.IsTrue(bool1.Equals(bool1));
            Assert.IsTrue(bool2.Equals(bool2));
            Assert.IsFalse(bool1.Equals(bool2));

            bool1.value = true;

            Assert.IsFalse(bool1.Equals(falseConstant));
            Assert.IsTrue(bool1.Equals(trueConstant));

            Assert.IsTrue(bool1.Equals(bool1));
            Assert.IsTrue(bool1.Equals(bool2));

            bool2.value = false;

            Assert.IsTrue(bool2.Equals(falseConstant));
            Assert.IsFalse(bool2.Equals(trueConstant));

            Assert.IsTrue(bool2.Equals(bool2));
            Assert.IsFalse(bool1.Equals(bool2));
        }

        [Test]
        public void GreaterThanTest () {

            BooleanParameter bool1 = new BooleanParameter() {
                name = "bool1",
                value = false
            };


            ConstantBooleanParameter falseConstant = new ConstantBooleanParameter() {
                value = false
            };

            Assert.Throws<NotSupportedException>(() => bool1.GreaterThan(falseConstant));
        }

        [Test]
        public void SmallerThanTest () {

            BooleanParameter bool1 = new BooleanParameter() {
                name = "bool1",
                value = false
            };


            ConstantBooleanParameter falseConstant = new ConstantBooleanParameter() {
                value = false
            };

            Assert.Throws<NotSupportedException>(() => bool1.SmallerThan(falseConstant));
        }
    }
}