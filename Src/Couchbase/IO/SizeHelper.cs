namespace Couchbase.IO
{
    public static class SizeHelper
    {
        // 20MB
        private const int SizeLimit = 20 * 1024 * 1024;

        public static void Check(byte[] request, string key)
        {
            if (request.Length > SizeLimit)
            {
                throw new ValueTooBigException($"The size of the request is too large. Key = {key}. Expecting less or equal {SizeLimit} bytes but was {request.Length}");
            }
        }
    }
}
