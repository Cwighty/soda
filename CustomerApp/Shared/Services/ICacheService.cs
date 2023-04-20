namespace CustomerApp.Shared.Services
{
    public interface ICacheService
    {
        void Add<T>(string key, T value);
        void Clear(string key);
        void Empty();
        T Get<T>(string key);
    }
}