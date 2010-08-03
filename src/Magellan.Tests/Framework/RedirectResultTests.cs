using System.Windows.Controls;
using Magellan.Mvc;
using Magellan.Routing;
using Moq;
using NUnit.Framework;
using Magellan;
using Magellan.Tests.Helpers;

namespace Magellan.Tests.Framework
{
    [TestFixture]
    public class RedirectResultTests
    {
        [Test]
        public void RedirectResultShouldUseSuppliedNavigator()
        {
            var request = RequestBuilder.CreateRequest();
            var newRequest = new RouteValueDictionary(new {controller = "Foo", action = "Bar", abc = "123"});
            var result = new RedirectResult(newRequest);
            result.Execute(request.BuildControllerContext());
            request.Navigator.Verify(x => x.ProcessRequest(It.IsAny<NavigationRequest>()));
        }
    }
}
