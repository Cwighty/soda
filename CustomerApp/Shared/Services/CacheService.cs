using MonkeyCache.FileStore;

namespace CustomerApp.Shared.Services
{
    public class CacheService : ICacheService
    {
        public void Add<T>(string key, T value)
        {
            Barrel.Current.Add(key, value, TimeSpan.FromMinutes(10));
        }

        public T Get<T>(string key)
        {
            return Barrel.Current.Get<T>(key);
        }

        public void Empty()
        {
            Barrel.Current.EmptyAll();
        }
    }
}
