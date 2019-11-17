using NUnit.Framework;
using UnityEngine;

namespace Helluys.FsmCore.Tests {

	[TestFixture]
	public partial class FiniteStateMachineTests {
		private FiniteStateMachine fsm;
		private TestFsmState state1, state2;
		private FsmTransition transition1to2, transition2to3, transition3to4, transition4to1;
		private FsmParameter triggerParameter, booleanParameter, integerParameter, floatParameter;

		[SetUp]
		public void SetUp() {
			fsm = ScriptableObject.CreateInstance<FiniteStateMachine>();

			state1 = ScriptableObject.CreateInstance<TestFsmState>();
			state1.name = "State1";
			state2 = ScriptableObject.CreateInstance<TestFsmState>();
			state2.name = "State2";

			fsm.AddStateInstance("One", state1);
			fsm.AddStateInstance("Two", state2);
			fsm.AddStateInstance("Three", state1);
			fsm.AddStateInstance("Four", state2);

			triggerParameter = FsmParameter.NewBoolean("testTrig");
			booleanParameter = FsmParameter.NewBoolean("testBool");
			integerParameter = FsmParameter.NewInteger("testInt");
			floatParameter = FsmParameter.NewFloat("testFloat");

			fsm.AddParameter(triggerParameter);
			fsm.AddParameter(booleanParameter);
			fsm.AddParameter(integerParameter);
			fsm.AddParameter(floatParameter);

			transition1to2 = new FsmTransition();
			transition1to2.AddCondition(FsmCondition.NewTrigger(triggerParameter));

			transition2to3 = new FsmTransition();
			FsmCondition equalsCondition = FsmCondition.NewEquals(booleanParameter, true);
			transition2to3.AddCondition(equalsCondition);

			transition3to4 = new FsmTransition();
			FsmCondition greaterCondition = FsmCondition.NewGreaterThan(integerParameter, 3);
			transition3to4.AddCondition(greaterCondition);

			transition4to1 = new FsmTransition();
			FsmCondition smallerCondition = FsmCondition.NewSmallerThan(floatParameter, -1f);
			transition4to1.AddCondition(smallerCondition);

			fsm.AddTransition("One", "Two", transition1to2);
			fsm.AddTransition("Two", "Three", transition2to3);
			fsm.AddTransition("Three", "Four", transition3to4);
			fsm.AddTransition("Four", "One", transition4to1);
		}

		[Test]
		public void FiniteStateMachineTest() {
			// Starts in One / State1
			Assert.AreEqual("One", fsm.currentStateInstanceName);

			Assert.AreEqual(0, state1.onEnterCount);
			Assert.AreEqual(0, state1.onStayCount);
			Assert.AreEqual(0, state1.onExitCount);

			Assert.AreEqual(0, state2.onEnterCount);
			Assert.AreEqual(0, state2.onStayCount);
			Assert.AreEqual(0, state2.onExitCount);

			// Stay in One
			fsm.Update();

			Assert.AreEqual("One", fsm.currentStateInstanceName);

			Assert.AreEqual(0, state1.onEnterCount);
			Assert.AreEqual(1, state1.onStayCount);
			Assert.AreEqual(0, state1.onExitCount);

			Assert.AreEqual(0, state2.onEnterCount);
			Assert.AreEqual(0, state2.onStayCount);
			Assert.AreEqual(0, state2.onExitCount);

			// Transition to Two
			fsm.GetParameter("testTrig").boolValue = true;
			fsm.Update();

			Assert.AreEqual("Two", fsm.currentStateInstanceName);

			Assert.AreEqual(0, state1.onEnterCount);
			Assert.AreEqual(1, state1.onStayCount);
			Assert.AreEqual(1, state1.onExitCount);

			Assert.AreEqual(1, state2.onEnterCount);
			Assert.AreEqual(1, state2.onStayCount);
			Assert.AreEqual(0, state2.onExitCount);

			// Stay in Two
			fsm.Update();
			Assert.AreEqual("Two", fsm.currentStateInstanceName);

			Assert.AreEqual(0, state1.onEnterCount);
			Assert.AreEqual(1, state1.onStayCount);
			Assert.AreEqual(1, state1.onExitCount);

			Assert.AreEqual(1, state2.onEnterCount);
			Assert.AreEqual(2, state2.onStayCount);
			Assert.AreEqual(0, state2.onExitCount);

			// Transition to three
			fsm.GetParameter("testBool").boolValue = true;
			fsm.Update();

			Assert.AreEqual("Three", fsm.currentStateInstanceName);

			Assert.AreEqual(1, state1.onEnterCount);
			Assert.AreEqual(2, state1.onStayCount);
			Assert.AreEqual(1, state1.onExitCount);

			Assert.AreEqual(1, state2.onEnterCount);
			Assert.AreEqual(2, state2.onStayCount);
			Assert.AreEqual(1, state2.onExitCount);

			// Stay in Three
			fsm.Update();

			Assert.AreEqual("Three", fsm.currentStateInstanceName);

			Assert.AreEqual(1, state1.onEnterCount);
			Assert.AreEqual(3, state1.onStayCount);
			Assert.AreEqual(1, state1.onExitCount);

			Assert.AreEqual(1, state2.onEnterCount);
			Assert.AreEqual(2, state2.onStayCount);
			Assert.AreEqual(1, state2.onExitCount);

			// Transition to Four
			fsm.GetParameter("testInt").intValue = 5;
			fsm.Update();

			Assert.AreEqual("Four", fsm.currentStateInstanceName);

			Assert.AreEqual(1, state1.onEnterCount);
			Assert.AreEqual(3, state1.onStayCount);
			Assert.AreEqual(2, state1.onExitCount);

			Assert.AreEqual(2, state2.onEnterCount);
			Assert.AreEqual(3, state2.onStayCount);
			Assert.AreEqual(1, state2.onExitCount);

			// Stay in Four
			fsm.Update();

			Assert.AreEqual("Four", fsm.currentStateInstanceName);

			Assert.AreEqual(1, state1.onEnterCount);
			Assert.AreEqual(3, state1.onStayCount);
			Assert.AreEqual(2, state1.onExitCount);

			Assert.AreEqual(2, state2.onEnterCount);
			Assert.AreEqual(4, state2.onStayCount);
			Assert.AreEqual(1, state2.onExitCount);

			// Transition to One
			fsm.GetParameter("testFloat").floatValue = -2f;
			fsm.Update();

			Assert.AreEqual("One", fsm.currentStateInstanceName);

			Assert.AreEqual(2, state1.onEnterCount);
			Assert.AreEqual(4, state1.onStayCount);
			Assert.AreEqual(2, state1.onExitCount);

			Assert.AreEqual(2, state2.onEnterCount);
			Assert.AreEqual(4, state2.onStayCount);
			Assert.AreEqual(2, state2.onExitCount);

			// Stay in One
			fsm.Update();

			Assert.AreEqual("One", fsm.currentStateInstanceName);

			Assert.AreEqual(2, state1.onEnterCount);
			Assert.AreEqual(5, state1.onStayCount);
			Assert.AreEqual(2, state1.onExitCount);

			Assert.AreEqual(2, state2.onEnterCount);
			Assert.AreEqual(4, state2.onStayCount);
			Assert.AreEqual(2, state2.onExitCount);

			// Immediate loop One -> Two -> Three -> Four -> One (all conditions met)
			fsm.GetParameter("testTrig").boolValue = true;
			fsm.Update();

			Assert.AreEqual("One", fsm.currentStateInstanceName);

			Assert.AreEqual(4, state1.onEnterCount);
			Assert.AreEqual(6, state1.onStayCount);
			Assert.AreEqual(4, state1.onExitCount);

			Assert.AreEqual(4, state2.onEnterCount);
			Assert.AreEqual(4, state2.onStayCount);
			Assert.AreEqual(4, state2.onExitCount);
		}
	}
}
