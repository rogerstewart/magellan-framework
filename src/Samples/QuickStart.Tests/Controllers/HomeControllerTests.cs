using NUnit.Framework;
using Quickstart.Controllers;
using Quickstart.Views.Home;

namespace QuickStart.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        [Test]
        public void AddTest()
        {
            var controller = new HomeController();
            controller.Add(1, 4);
            var model = (AddModel)controller.Model;

            Assert.AreEqual(1, model.A);
            Assert.AreEqual(4, model.B);
            Assert.AreEqual(5, model.Result);
        }
    }
}