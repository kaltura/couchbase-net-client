using System;

namespace Couchbase.IO
{
    public sealed class ValueTooBigException : Exception
    {
        public ValueTooBigException(string message) : base(message)
        {
        }
    }
}
