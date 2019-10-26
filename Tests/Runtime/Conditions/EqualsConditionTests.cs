using Helluys.FsmCore.Conditions;
using Helluys.FsmCore.Parameters;
using NUnit.Framework;

namespace Helluys.FsmCore.Tests.Conditions
{
    [TestFixture]
    public class EqualsConditionTests
    {
        [Test]
        public void BooleanEvaluationTest() {
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

            Assert.IsTrue(new EqualsCondition(bool1, falseConstant).Evaluate());
            Assert.IsFalse(new EqualsCondition(bool2, falseConstant).Evaluate());
            Assert.IsFalse(new EqualsCondition(bool1, trueConstant).Evaluate());
            Assert.IsTrue(new EqualsCondition(bool2, trueConstant).Evaluate());
        }

        [Test] 
        public void BooleanContainsTest() {
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

            EqualsCondition equalsCondition1 = new EqualsCondition(bool1, falseConstant);
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

            ConstantIntegerParameter const1 = new ConstantIntegerParameter() {
                value = 1
            };

            ConstantIntegerParameter const2 = new ConstantIntegerParameter() {
                value = 2
            };

            Assert.IsTrue(new EqualsCondition(int1, const1).Evaluate());
            Assert.IsFalse(new EqualsCondition(int2, const1).Evaluate());
            Assert.IsFalse(new EqualsCondition(int1, const2).Evaluate());
            Assert.IsTrue(new EqualsCondition(int2, const2).Evaluate());
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

            EqualsCondition equalsCondition1 = new EqualsCondition(int1, const1);
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

            ConstantFloatParameter const1 = new ConstantFloatParameter() {
                value = 1f
            };

            ConstantFloatParameter const2 = new ConstantFloatParameter() {
                value = 2f
            };

            Assert.IsTrue(new EqualsCondition(float1, const1).Evaluate());
            Assert.IsFalse(new EqualsCondition(float2, const1).Evaluate());
            Assert.IsFalse(new EqualsCondition(float1, const2).Evaluate());
            Assert.IsTrue(new EqualsCondition(float2, const2).Evaluate());
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

            EqualsCondition equalsCondition1 = new EqualsCondition(float1, const1);
            Assert.IsTrue(equalsCondition1.Contains(float1));
            Assert.IsFalse(equalsCondition1.Contains(float2));
        }
    }
}
