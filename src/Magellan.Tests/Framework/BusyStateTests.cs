using System.Collections.Generic;
using Magellan.Framework;
using NUnit.Framework;

namespace Magellan.Tests.Framework
{
    [TestFixture]
    public class BusyStateTests
    {
        [Test]
        public void WhenInitializedIsNotBusy()
        {
            var busyState = new BusyState();
            Assert.IsFalse(busyState.IsBusy);
        }

        [Test]
        public void EnteringSetsIsBusyToTrue()
        {
            var busyState = new BusyState();
            busyState.Enter();
            Assert.IsTrue(busyState.IsBusy);
        }

        [Test]
        public void ExitingSetsIsBusyToFalseAgain()
        {
            var busyState = new BusyState();
            busyState.Enter();
            busyState.Exit();
            Assert.IsFalse(busyState.IsBusy);
        }

        [Test]
        public void MustCallExitSameNumberOfTimesAsEnter()
        {
            var busyState = new BusyState();
            busyState.Enter();
            busyState.Enter();
            busyState.Enter();
            busyState.Exit();
            Assert.IsTrue(busyState.IsBusy);
            busyState.Exit();
            Assert.IsTrue(busyState.IsBusy);
            busyState.Exit();
            Assert.IsFalse(busyState.IsBusy);
        }

        [Test]
        public void RaisesPropertyChangedOnIsBusyChanged()
        {
            var changes = new Queue<string>();

            var busyState = new BusyState();
            busyState.PropertyChanged += (x, e) => changes.Enqueue(e.PropertyName);
            
            busyState.Enter();
            Assert.AreEqual("IsBusy", changes.Dequeue());
            busyState.Enter();
            Assert.AreEqual(0, changes.Count);

            busyState.Exit();
            Assert.AreEqual(0, changes.Count);
            busyState.Exit();
            Assert.AreEqual("IsBusy", changes.Dequeue());

            Assert.AreEqual(0, changes.Count);
        }
    }
}
