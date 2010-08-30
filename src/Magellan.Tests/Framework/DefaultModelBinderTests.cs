using System;
using Magellan;
using Magellan.Framework;
using Magellan.Routing;
using Magellan.Tests.Helpers;
using NUnit.Framework;

namespace Magellan.Tests.Framework
{
    [TestFixture]
    public class DefaultModelBinderTests
    {
        protected object Bind(string parameterName, Type parameterType, object parameters)
        {
            var modelBinder = new DefaultModelBinder();
            var result = modelBinder.BindModel(RequestBuilder.CreateRequest().BuildControllerContext(), new ModelBindingContext("foo", parameterType, new RouteValueDictionary(parameters)));
            return result;
        }

        [Test]
        public void ShouldMatchParameterNames()
        {
            var result = Bind("foo", typeof(string), new { foo = "ABC" });
            Assert.AreEqual("ABC", result);
        }

        [Test]
        public void ParameterNamesAreNotCaseSensitive()
        {
            var result = Bind("FOO", typeof(string), new { foo = "ABC" });
            Assert.AreEqual("ABC", result);
        }

        [Test]
        public void ShouldAttemptToUseTypeConverters()
        {
            var result = Bind("FOO", typeof(string), new { foo = 10 });
            Assert.AreEqual("10", result);

            var result2 = Bind("FOO", typeof(int), new { foo = "10" });
            Assert.AreEqual(10, result2);
        }

        [Test]
        public void ShouldHandleNullValues()
        {
            var result = Bind("FOO", typeof(string), new { foo = (string)null });
            Assert.IsNull(result);
        }

        [Test]
        public void ShouldHandleNullValuesOnValueTypes()
        {
            var result = Bind("FOO", typeof(int), new { foo = (string)null });
            Assert.AreEqual(0, result);
        }

        [Test]
        public void UnmatchedParametersWillThrowException()
        {
            Assert.Throws<ArgumentException>(() => Bind("Foo", typeof(string), new { }));
        }
    }
}
