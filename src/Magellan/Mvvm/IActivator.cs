using System;
using Magellan.Routing;

namespace Magellan.Mvvm
{
    public interface IActivator
    {
        object Create(Type typeToCreate, RouteValueDictionary parameters);
    }
}
