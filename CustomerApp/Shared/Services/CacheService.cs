using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApp.Shared.Services
{
    public class CacheService : ICacheService
    {
        public void Add<T>(string key, T value)
        {
            Barrel.Current.Add(key, value, TimeSpan.FromDays(1));
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
