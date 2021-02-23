using System.Collections.Generic;

namespace SocialNetwork.Extensions.Collections
{
    public class CollectionResult<T>
    {
        public int TotalCount { get; set; }

        public IList<T> Data { get; set; }

        public CollectionResult() { }

        public CollectionResult(int totalCount, IList<T> data)
        {
            TotalCount = totalCount;
            Data = data;
        }
    }
}