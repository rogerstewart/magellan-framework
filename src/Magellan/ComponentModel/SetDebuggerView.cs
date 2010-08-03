using System.Diagnostics;
using System.Linq;

namespace Magellan.ComponentModel
{
    /// <summary>
    /// A debugger visualizer for <see cref="Set{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of item in the set.</typeparam>
    internal sealed class SetDebuggerView<T> where T : class
    {
        private readonly Set<T> _collection;

        public SetDebuggerView(Set<T> collection)
        {
            _collection = collection;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public T[] Items
        {
            get
            {
                return _collection.ToArray();
            }
        }
    }
}