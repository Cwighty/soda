namespace CustomerApp.Services
{
    public interface ICacheService
    {
        void Add<T>(string key, T value);
        void Empty();
        T Get<T>(string key);
    }
}