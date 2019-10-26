using Helluys.FsmCore.Conditions;
using Helluys.FsmCore.Parameters;
using NUnit.Framework;
using UnityEngine;

namespace Helluys.FsmCore.Tests
{
    [TestFixture]
    public partial class FiniteStateMachineTests
    {
        private FiniteStateMachine fsm;

        private TestFsmState state1;
        private TestFsmState state2;

        private FsmTransition transition1to2;
        private FsmTransition transition2to3;
        private FsmTransition transition3to4;
        private FsmTransition transition4to1;

        private TriggerParameter triggerParameter;
        private BooleanParameter booleanParameter;
        private IntegerParameter integerParameter;
        private FloatParameter floatParameter;

        [SetUp]
        public void SetUp () {
            fsm = ScriptableObject.CreateInstance<FiniteStateMachine>();

            state1 = ScriptableObject.CreateInstance<TestFsmState>();
            state1.name = "State1";
            state2 = ScriptableObject.CreateInstance<TestFsmState>();
            state2.name = "State2";

            fsm.AddStateInstance("One", state1);
            fsm.AddStateInstance("Two", state2);

            fsm.AddStateInstance("Three", state1);
            fsm.AddStateInstance("Four", state2);

            triggerParameter = new TriggerParameter() { name = "testTrigger" };
            booleanParameter = new BooleanParameter() { name = "testBool", value = false };
            integerParameter = new IntegerParameter() { name = "testInt", value = 0 };
            floatParameter = new FloatParameter() { name = "testFloat", value = 0f };

            fsm.AddParameter(triggerParameter);
            fsm.AddParameter(booleanParameter);
            fsm.AddParameter(integerParameter);
            fsm.AddParameter(floatParameter);

            transition1to2 = new FsmTransition();
            transition1to2.AddCondition(new TriggerCondition(triggerParameter));

            transition2to3 = new FsmTransition();
            transition2to3.AddCondition(new EqualsCondition(booleanParameter, new ConstantBooleanParameter() { value = true }));

            transition3to4 = new FsmTransition();
            transition3to4.AddCondition(new GreaterThanCondition(integerParameter, new ConstantIntegerParameter() { value = 3 }));

            transition4to1 = new FsmTransition();
            transition4to1.AddCondition(new SmallerThanCondition(floatParameter, new ConstantFloatParameter() { value = -1f }));

            fsm.AddTransition("One", "Two", transition1to2);
            fsm.AddTransition("Two", "Three", transition2to3);
            fsm.AddTransition("Three", "Four", transition3to4);
            fsm.AddTransition("Four", "One", transition4to1);
        }

        [Test]
        public void FiniteStateMachineTest () {
            // Starts in One / State1
            Assert.AreEqual("One", fsm.currentStateInstanceName);

            Assert.AreEqual(0, state1.onEnterCount);
            Assert.AreEqual(0, state1.onStayCount);
            Assert.AreEqual(0, state1.onExitCount);

            Assert.AreEqual(0, state2.onEnterCount);
            Assert.AreEqual(0, state2.onStayCount);
            Assert.AreEqual(0, state2.onExitCount);

            // Transition to Two
            (fsm.GetParameter("trigger") as TriggerParameter).Set();
            fsm.Update();

            Assert.AreEqual("Two", fsm.currentStateInstanceName);

            Assert.AreEqual(0, state1.onEnterCount);
            Assert.AreEqual(0, state1.onStayCount);
            Assert.AreEqual(1, state1.onExitCount);

            Assert.AreEqual(1, state2.onEnterCount);
            Assert.AreEqual(0, state2.onStayCount);
            Assert.AreEqual(0, state2.onExitCount);

            // Stay in Two
            fsm.Update();
            Assert.AreEqual("Two", fsm.currentStateInstanceName);

            Assert.AreEqual(0, state1.onEnterCount);
            Assert.AreEqual(0, state1.onStayCount);
            Assert.AreEqual(1, state1.onExitCount);

            Assert.AreEqual(1, state2.onEnterCount);
            Assert.AreEqual(1, state2.onStayCount);
            Assert.AreEqual(0, state2.onExitCount);

            // Transition to three
            (fsm.GetParameter("boolean") as BooleanParameter).Set(true);
            fsm.Update();

            Assert.AreEqual("Three", fsm.currentStateInstanceName);

            Assert.AreEqual(1, state1.onEnterCount);
            Assert.AreEqual(0, state1.onStayCount);
            Assert.AreEqual(1, state1.onExitCount);

            Assert.AreEqual(1, state2.onEnterCount);
            Assert.AreEqual(1, state2.onStayCount);
            Assert.AreEqual(1, state2.onExitCount);

            // Stay in Three
            fsm.Update();

            Assert.AreEqual("Three", fsm.currentStateInstanceName);

            Assert.AreEqual(1, state1.onEnterCount);
            Assert.AreEqual(1, state1.onStayCount);
            Assert.AreEqual(1, state1.onExitCount);

            Assert.AreEqual(1, state2.onEnterCount);
            Assert.AreEqual(1, state2.onStayCount);
            Assert.AreEqual(1, state2.onExitCount);

            // Transition to Four
            (fsm.GetParameter("integer") as IntegerParameter).Set(5);
            fsm.Update();

            Assert.AreEqual("Four", fsm.currentStateInstanceName);

            Assert.AreEqual(1, state1.onEnterCount);
            Assert.AreEqual(1, state1.onStayCount);
            Assert.AreEqual(2, state1.onExitCount);

            Assert.AreEqual(2, state2.onEnterCount);
            Assert.AreEqual(1, state2.onStayCount);
            Assert.AreEqual(1, state2.onExitCount);

            // Stay in Four
            fsm.Update();

            Assert.AreEqual("Four", fsm.currentStateInstanceName);

            Assert.AreEqual(1, state1.onEnterCount);
            Assert.AreEqual(1, state1.onStayCount);
            Assert.AreEqual(2, state1.onExitCount);

            Assert.AreEqual(2, state2.onEnterCount);
            Assert.AreEqual(2, state2.onStayCount);
            Assert.AreEqual(1, state2.onExitCount);

            // Transition to One
            (fsm.GetParameter("float") as FloatParameter).Set(-2f);
            fsm.Update();

            Assert.AreEqual("One", fsm.currentStateInstanceName);

            Assert.AreEqual(2, state1.onEnterCount);
            Assert.AreEqual(1, state1.onStayCount);
            Assert.AreEqual(2, state1.onExitCount);

            Assert.AreEqual(2, state2.onEnterCount);
            Assert.AreEqual(2, state2.onStayCount);
            Assert.AreEqual(2, state2.onExitCount);

            // Stay in One
            fsm.Update();

            Assert.AreEqual("One", fsm.currentStateInstanceName);

            Assert.AreEqual(2, state1.onEnterCount);
            Assert.AreEqual(2, state1.onStayCount);
            Assert.AreEqual(2, state1.onExitCount);

            Assert.AreEqual(2, state2.onEnterCount);
            Assert.AreEqual(2, state2.onStayCount);
            Assert.AreEqual(2, state2.onExitCount);
        }
    }
}
