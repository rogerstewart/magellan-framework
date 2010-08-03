using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

namespace Magellan.Tests.Helpers.TypeGeneration
{
    public interface ITypeContributor
    {
        void Contribute(TypeBuilder typeBuilder, FieldBuilder implementationField, RuntimeImplementation implementation);
        void Verify();
    }
}
