using System;

namespace Magellan.Framework
{
    public interface IFlasher
    {
        void Flash(string message);
        void Flash(string message, TimeSpan disappears);
        void Flash(Flash flash);
        void ClearFlashes();
    }
}