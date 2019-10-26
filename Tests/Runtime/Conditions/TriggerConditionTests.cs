using Helluys.FsmCore.Conditions;
using Helluys.FsmCore.Parameters;
using NUnit.Framework;

namespace Helluys.FsmCore.Tests.Conditions
{
    [TestFixture]
    public class TriggerConditionTests
    {
        [Test]
        public void EvaluationTest () {
            TriggerParameter trig1 = new TriggerParameter() {
                name = "trig1",
            };

            TriggerCondition triggerCondition1 = new TriggerCondition(trig1);

            Assert.IsFalse(triggerCondition1.Evaluate());

            trig1.Set();
            Assert.IsTrue(new TriggerCondition(trig1).Evaluate());
            Assert.IsFalse(new TriggerCondition(trig1).Evaluate());
        }

        [Test]
        public void ContainsTest () {
            TriggerParameter trig1 = new TriggerParameter() {
                name = "trig1",
            };

            TriggerParameter trig2 = new TriggerParameter() {
                name = "trig2",
            };

            TriggerCondition triggerCondition1 = new TriggerCondition(trig1);

            Assert.IsTrue(triggerCondition1.Contains(trig1));
            Assert.IsFalse(triggerCondition1.Contains(trig2));
        }
    }
}
