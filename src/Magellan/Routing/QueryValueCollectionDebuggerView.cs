using System.Collections.Generic;
using System.Diagnostics;

namespace Magellan.Routing
{
    internal sealed class QueryValueCollectionDebuggerView
    {
        private readonly QueryValueCollection _collection;

        public QueryValueCollectionDebuggerView(QueryValueCollection collection)
        {
            _collection = collection;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public KeyValuePair<string, string>[] Items
        {
            get
            {
                var array = new KeyValuePair<string, string>[_collection.Count];
                _collection.CopyTo(array, 0);
                return array;
            }
        }
    }
}