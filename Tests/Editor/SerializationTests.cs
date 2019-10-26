using Helluys.FsmCore.Conditions;
using Helluys.FsmCore.Parameters;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Helluys.FsmCore.Tests
{
    [TestFixture]
    public class SerializationTests
    {
        private FiniteStateMachine fsm;

        private const string ASSET_PATH = "Packages/com.helluys.fsmcore/Tests/Editor/Assets/";

        [SetUp]
        public void SetUp () {
            fsm = ScriptableObject.CreateInstance<FiniteStateMachine>();

            LoggerFsmState state1 = ScriptableObject.CreateInstance<LoggerFsmState>();
            state1.name = "State1";
            AssetDatabase.CreateAsset(state1, ASSET_PATH + "State1.asset");

            LoggerFsmState state2 = ScriptableObject.CreateInstance<LoggerFsmState>();
            state2.name = "State2";
            AssetDatabase.CreateAsset(state2, ASSET_PATH + "State2.asset");
            AssetDatabase.SaveAssets();

            fsm.AddStateInstance("One", state1);
            fsm.AddStateInstance("Two", state2);

            fsm.AddStateInstance("Three", state1);
            fsm.AddStateInstance("Four", state2);

            TriggerParameter triggerParameter = new TriggerParameter() { name = "testTrigger" };
            FsmParameter booleanParameter = new BooleanParameter() { name = "testBool", value = false };
            FsmParameter integerParameter = new IntegerParameter() { name = "testInt", value = 0 };
            FsmParameter floatParameter = new FloatParameter() { name = "testFloat", value = 0f };

            fsm.AddParameter(triggerParameter);
            fsm.AddParameter(booleanParameter);
            fsm.AddParameter(integerParameter);
            fsm.AddParameter(floatParameter);

            FsmTransition transition1to2 = new FsmTransition();
            transition1to2.AddCondition(new TriggerCondition(triggerParameter));

            FsmTransition transition2to3 = new FsmTransition();
            transition2to3.AddCondition(new EqualsCondition(booleanParameter, new ConstantBooleanParameter() { value = true }));

            FsmTransition transition3to4 = new FsmTransition();
            transition3to4.AddCondition(new GreaterThanCondition(integerParameter, new ConstantIntegerParameter() { value = 3 }));

            FsmTransition transition4to1 = new FsmTransition();
            transition4to1.AddCondition(new SmallerThanCondition(floatParameter, new ConstantFloatParameter() { value = -1f }));

            fsm.AddTransition("One", "Two", transition1to2);
            fsm.AddTransition("Two", "Three", transition2to3);
            fsm.AddTransition("Three", "Four", transition3to4);
            fsm.AddTransition("Four", "One", transition4to1);

            // Clean up if asset already exists
            AssetDatabase.DeleteAsset(ASSET_PATH);
        }

        [Test]
        public void SerializationTest () {
            AssetDatabase.CreateAsset(fsm, ASSET_PATH + "SavedFsm.asset");
            AssetDatabase.SaveAssets();

            FiniteStateMachine loadedFsm = AssetDatabase.LoadAssetAtPath<FiniteStateMachine>(ASSET_PATH + "SavedFsm.asset");

            Assert.AreEqual(fsm.GetParameters(), loadedFsm.GetParameters());
            Assert.AreEqual(fsm.states, loadedFsm.states);
        }

        [TearDown]
        public void TearDown() {
            // Clean up
            //AssetDatabase.DeleteAsset(ASSET_PATH);
        }
    }
}
