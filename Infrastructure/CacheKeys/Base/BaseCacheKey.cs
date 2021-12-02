namespace Infrastructure.CacheKeys.Base
{
    public class BaseCacheKey<T> where T : class
    {
        public string ListKey => $"{typeof(T).Name}-List";

        public string SelectListKey => $"{typeof(T).Name}-SelectList";

        public string GetKey(long brandId) => $"{typeof(T).Name}-{brandId}";

        public string GetDetailsKey(long brandId) => $"{typeof(T).Name}-{brandId}";
    }
}
