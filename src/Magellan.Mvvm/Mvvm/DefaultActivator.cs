using System;
using Magellan.Routing;

namespace Magellan.Mvvm
{
    public class DefaultActivator : IActivator
    {
        public object Create(Type typeToCreate, RouteValueDictionary parameters)
        {
            return Activator.CreateInstance(typeToCreate);
        }
    }
}