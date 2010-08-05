using Magellan.Mvc;
using Magellan.Tests.Helpers;
using NUnit.Framework;
using Magellan;
using Magellan.Tests.Helpers.TypeGeneration;
using System.Threading;

namespace Magellan.Tests.Framework
{
    [TestFixture]
    public class AsyncActionInvokerTests
    {
        protected TypeBuilder<Controller> Controller { get; set; }

        [SetUp]
        public void SetUp()
        {
            Controller = new TypeBuilder<Controller>("ExampleController");
        }

        [TearDown]
        public void TearDown()
        {
            Controller.VerifyAll();
        }

        [Test]
        public void ShouldExecuteOnBackgroundThread()
        {
            var testThreadId = Thread.CurrentThread.ManagedThreadId;
            var waitHandle = new AutoResetEvent(false);

            Controller.Method("ShowCustomer").Returns((int a) =>
                {
                    Assert.AreNotEqual(testThreadId, Thread.CurrentThread.ManagedThreadId);
                    waitHandle.Set();
                    return new DoNothingResult();
                }).MustBeCalled();

            Controller.Instance.ActionInvoker = new AsyncActionInvoker();
            Controller.Instance.Execute("ShowCustomer", new { a = 3 });

            waitHandle.WaitOne();
        }

        [Test]
        public void ShouldSetNameOfWorkerThread()
        {
            var waitHandle = new AutoResetEvent(false);

            Controller.Method("ShowCustomer").Returns((int a) =>
            {
                Assert.IsTrue(Thread.CurrentThread.Name.StartsWith("Navigation request: "));
                waitHandle.Set();
                return new DoNothingResult();
            }).MustBeCalled();

            Controller.Instance.ActionInvoker = new AsyncActionInvoker();
            Controller.Instance.Execute("ShowCustomer", new { a = 3 });

            waitHandle.WaitOne();
        }
    }
}
