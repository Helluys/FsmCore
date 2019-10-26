using System;
using Helluys.FsmCore.Parameters;
using NUnit.Framework;

namespace Helluys.FsmCore.Tests.Parameters
{
    [TestFixture]
    public class FloatParameterTests
    {
        [Test]
        public void GetSetTest() {
            FloatParameter float1 = new FloatParameter() {
                name = "float1",
                value = 1f
            };

            Assert.IsTrue(Math.Abs(float1.Get() - 1f) < float.Epsilon);

            float1.Set(2f);
            Assert.IsTrue(Math.Abs(float1.Get() - 2f) < float.Epsilon);
        }

        [Test]
        public void EqualsTest () {
            FloatParameter float1 = new FloatParameter() {
                name = "float1",
                value = 1f
            };

            FloatParameter float2 = new FloatParameter() {
                name = "float2",
                value = 2f
            };

            ConstantFloatParameter const1 = new ConstantFloatParameter() {
                value = 1f
            };

            ConstantFloatParameter const2 = new ConstantFloatParameter() {
                value = 2f
            };

            Assert.IsTrue(float1.Equals(const1));
            Assert.IsFalse(float1.Equals(const2));

            Assert.IsFalse(float2.Equals(const1));
            Assert.IsTrue(float2.Equals(const2));

            Assert.IsTrue(float1.Equals(float1));
            Assert.IsTrue(float2.Equals(float2));
            Assert.IsFalse(float1.Equals(float2));

            float1.value = 2;

            Assert.IsFalse(float1.Equals(const1));
            Assert.IsTrue(float1.Equals(const2));

            Assert.IsTrue(float1.Equals(float2));

            float2.value = 3;

            Assert.IsFalse(float2.Equals(const1));
            Assert.IsFalse(float2.Equals(const2));

            Assert.IsTrue(float2.Equals(float2));

            Assert.IsFalse(float1.Equals(float2));
        }

        [Test]
        public void GreaterThanTest () {

            FloatParameter float1 = new FloatParameter() {
                name = "float1",
                value = 1f
            };


            ConstantFloatParameter const0 = new ConstantFloatParameter() {
                value = 0f
            };
            ConstantFloatParameter const1 = new ConstantFloatParameter() {
                value = 1f
            };
            ConstantFloatParameter const2 = new ConstantFloatParameter() {
                value = 2f
            };

            Assert.IsTrue(float1.GreaterThan(const0));
            Assert.IsFalse(float1.GreaterThan(const1));
            Assert.IsFalse(float1.GreaterThan(const2));
        }

        [Test]
        public void SmallerThanTest () {

            FloatParameter float1 = new FloatParameter() {
                name = "float1",
                value = 1f
            };


            ConstantFloatParameter const0 = new ConstantFloatParameter() {
                value = 0f
            };
            ConstantFloatParameter const1 = new ConstantFloatParameter() {
                value = 1f
            };
            ConstantFloatParameter const2 = new ConstantFloatParameter() {
                value = 2f
            };

            Assert.IsFalse(float1.SmallerThan(const0));
            Assert.IsFalse(float1.SmallerThan(const1));
            Assert.IsTrue(float1.SmallerThan(const2));
        }
    }
}