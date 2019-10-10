namespace BusCache.Services
{
    public interface ICacheService
    {
        object Get(string key);
        void Set(string key, object value);
        bool TryGet(string key, out object value);
    }
}