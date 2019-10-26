using System;
using Helluys.FsmCore.Parameters;
using NUnit.Framework;

namespace Helluys.FsmCore.Tests.Parameters
{
    [TestFixture]
    public class TriggerParameterTests
    {
        [Test]
        public void GetSetTest () {
            TriggerParameter trig1 = new TriggerParameter() {
                name = "trig1",
            };

            Assert.IsFalse(trig1.Get());

            trig1.Set();

            Assert.IsTrue(trig1.Get());
            Assert.IsFalse(trig1.Get());
        }

        [Test]
        public void EqualsTest () {
            TriggerParameter trig1 = new TriggerParameter() {
                name = "trig1",
            };

            Assert.Throws<NotSupportedException>(() => trig1.Equals(new ConstantBooleanParameter()));
        }

        [Test]
        public void GreaterThanTest () {
            TriggerParameter trig1 = new TriggerParameter() {
                name = "trig1",
            };

            Assert.Throws<NotSupportedException>(() => trig1.GreaterThan(new ConstantBooleanParameter()));
        }

        [Test]
        public void SmallerThanTest () {
            TriggerParameter trig1 = new TriggerParameter() {
                name = "trig1",
            };

            Assert.Throws<NotSupportedException>(() => trig1.SmallerThan(new ConstantBooleanParameter()));
        }
    }
}