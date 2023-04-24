namespace CustomerApp.Shared.Services
{
    public interface IUserService
    {
        Task DeleteAccount();
        Task<Customer> GetCustomer();
        bool IsLoggedIn();
        Task Login(string email, string password);
        Task Logout();
        Task Register(string email, string password, string name);
        Task UpdateCustomer(Customer customer);
    }
}