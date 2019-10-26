using System;
using Helluys.FsmCore.Conditions;
using Helluys.FsmCore.Parameters;
using NUnit.Framework;

namespace Helluys.FsmCore.Tests.Conditions
{
    [TestFixture]
    public class SmallerThanConditionTests
    {
        [Test]
        public void BooleanEvaluationTest () {
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

            Assert.Throws<NotSupportedException>(() => new SmallerThanCondition(bool1, falseConstant).Evaluate());
            Assert.Throws<NotSupportedException>(() => new SmallerThanCondition(bool2, falseConstant).Evaluate());
            Assert.Throws<NotSupportedException>(() => new SmallerThanCondition(bool1, trueConstant).Evaluate());
            Assert.Throws<NotSupportedException>(() => new SmallerThanCondition(bool2, trueConstant).Evaluate());
        }

        [Test]
        public void BooleanContainsTest () {
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

            SmallerThanCondition equalsCondition1 = new SmallerThanCondition(bool1, falseConstant);
            Assert.IsTrue(equalsCondition1.Contains(bool1));
            Assert.IsFalse(equalsCondition1.Contains(bool2));
        }

        [Test]
        public void IntegerEvaluationTest () {
            IntegerParameter int1 = new IntegerParameter() {
                name = "int1",
                value = 1
            };
            IntegerParameter int2 = new IntegerParameter() {
                name = "int2",
                value = 2
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

            Assert.IsFalse(new SmallerThanCondition(int1, const0).Evaluate());
            Assert.IsFalse(new SmallerThanCondition(int1, const1).Evaluate());
            Assert.IsTrue(new SmallerThanCondition(int1, const2).Evaluate());
            Assert.IsFalse(new SmallerThanCondition(int2, const0).Evaluate());
            Assert.IsFalse(new SmallerThanCondition(int2, const1).Evaluate());
            Assert.IsFalse(new SmallerThanCondition(int2, const2).Evaluate());
        }

        [Test]
        public void IntegerContainsTest () {
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

            SmallerThanCondition equalsCondition1 = new SmallerThanCondition(int1, const1);
            Assert.IsTrue(equalsCondition1.Contains(int1));
            Assert.IsFalse(equalsCondition1.Contains(int2));
        }

        [Test]
        public void FloatEvaluationTest () {
            FloatParameter float1 = new FloatParameter() {
                name = "float1",
                value = 1f
            };
            FloatParameter float2 = new FloatParameter() {
                name = "float2",
                value = 2f
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

            Assert.IsFalse(new SmallerThanCondition(float1, const0).Evaluate());
            Assert.IsFalse(new SmallerThanCondition(float1, const1).Evaluate());
            Assert.IsTrue(new SmallerThanCondition(float1, const2).Evaluate());
            Assert.IsFalse(new SmallerThanCondition(float2, const0).Evaluate());
            Assert.IsFalse(new SmallerThanCondition(float2, const1).Evaluate());
            Assert.IsFalse(new SmallerThanCondition(float2, const2).Evaluate());
        }

        [Test]
        public void FloatContainsTest () {
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

            SmallerThanCondition equalsCondition1 = new SmallerThanCondition(float1, const1);
            Assert.IsTrue(equalsCondition1.Contains(float1));
            Assert.IsFalse(equalsCondition1.Contains(float2));
        }
    }
}
