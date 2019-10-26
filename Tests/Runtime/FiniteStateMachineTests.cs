using Helluys.FsmCore.Conditions;
using Helluys.FsmCore.Parameters;
using NUnit.Framework;
using UnityEngine;
using Assert = NUnit.Framework.Assert;

namespace Helluys.FsmCore.Tests
{
    [TestFixture]
    public class FiniteStateMachineTests
    {
        private class TestFsmState : FsmState
        {
            public int onEnterCount = 0;
            public int onStayCount = 0;
            public int onExitCount = 0;

            public override void OnEnter () {
                onEnterCount++;
            }

            public override void OnStay () {
                onStayCount++;
            }

            public override void OnExit () {
                onExitCount++;
            }

            public void Reset () {
                onEnterCount = 0;
                onStayCount = 0;
                onExitCount = 0;
            }
        }

        private FiniteStateMachine fsm;
        private TriggerParameter trigger11to21;
        private FsmTransition transition11to21;

        [SetUp]
        public void SetUp() {
            fsm = ScriptableObject.CreateInstance<FiniteStateMachine>();

            FsmState state1 = ScriptableObject.CreateInstance<TestFsmState>();
            state1.name = "State1";
            FsmState state2 = ScriptableObject.CreateInstance<TestFsmState>();
            state2.name = "State2";

            fsm.AddStateInstance("State11", state1);
            fsm.AddStateInstance("State12", state1);

            fsm.AddStateInstance("State21", state2);
            fsm.AddStateInstance("State22", state2);

            trigger11to21 = new TriggerParameter() { name = "testTrigger" };
            transition11to21 = new FsmTransition();
            transition11to21.AddCondition(new TriggerCondition(trigger11to21));

            fsm.AddTransition("State11", "State21", transition11to21);
        }

        [Test]
        public void FiniteStateMachineTest () {
            Assert.AreEqual("State11", fsm.currentStateInstanceName);
            Assert.AreEqual("State1", fsm.currentState.name);

            trigger11to21.Set();

            fsm.Update();

            Assert.AreEqual("State21", fsm.currentStateInstanceName);
            Assert.AreEqual("State2", fsm.currentState.name);
        }
    }
}
