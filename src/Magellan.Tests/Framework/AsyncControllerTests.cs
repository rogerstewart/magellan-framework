using Magellan;
using Magellan.Mvc;
using NUnit.Framework;

namespace Magellan.Tests.Framework
{
    [TestFixture]
    public class AsyncControllerTests
    {
        #region SUT

        private class MyController : AsyncController
        {
        }

        #endregion

        [Test]
        public void ShouldSetActionInvoker()
        {
            var controller = new AsyncController();
            Assert.IsInstanceOf<AsyncActionInvoker>(controller.ActionInvoker);
        }
    }
}
