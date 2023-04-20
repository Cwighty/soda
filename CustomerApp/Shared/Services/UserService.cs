using Client = Supabase.Client;

namespace CustomerApp.Shared.Services;

public class UserService
{
    private const string TOKEN_CACHE_KEY = "authToken";
    private readonly Client client;
    private readonly IMapper mapper;
    private readonly ICacheService cacheService;

    public UserService(Client client, IMapper mapper, ICacheService cacheService)
    {
        this.client = client;
        this.mapper = mapper;
        this.cacheService = cacheService;
        var cachedToken = cacheService.Get<string>(TOKEN_CACHE_KEY);
        if (cachedToken != null)
        {
            client.Auth.SetAuth(cachedToken);
        }
    }

    public async Task<Customer> GetCustomer()
    {
        if (!IsLoggedIn())
        {
            return null;
        }
        var response = await client.From<CustomerData>().Get();
        return mapper.Map<Customer>(response.Models.FirstOrDefault());
    }

    public bool IsLoggedIn()
    {
        return client.Auth.CurrentSession != null;
    }

    public async Task Login(string email, string password)
    {
        var url = await client.Auth.SignIn(Supabase.Gotrue.Constants.Provider.Google, "https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/userinfo.profile");
        try
        {
            WebAuthenticatorResult authResult = await WebAuthenticator.Default.AuthenticateAsync(
                new Uri(url),
                new Uri("soda://"));


            string accessToken = authResult?.AccessToken;
            client.Auth.SetAuth(accessToken);
            cacheService.Add(TOKEN_CACHE_KEY, accessToken);
        }
        catch
        {
        }
        //var response = await client.Auth.SignIn(email, password);
    }

    public async Task Register(string email, string password, string name)
    {
        var response = await client.Auth.SignUp(email, password);
        if (response != null)
        {
            var customer = new CustomerData
            {
                Id = response.User.Id,
                Name = name,
                Email = email
            };
            await client.From<CustomerData>().Insert(customer);
        }
    }



    public async Task Logout()
    {
        await client.Auth.SignOut();
        cacheService.Clear(TOKEN_CACHE_KEY);
    }

    public async Task UpdateCustomer(Customer customer)
    {
        var customerData = mapper.Map<CustomerData>(customer);
        await client.From<CustomerData>().Where(c => c.Id == customer.Id).Update(customerData);
    }

    public async Task DeleteAccount()
    {
        var customer = await GetCustomer();
        await client.From<CustomerData>().Where(c => c.Id == customer.Id).Delete();
        await client.Rpc("delete_user", null);
    }
}
