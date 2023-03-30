using Client = Supabase.Client;
using AutoMapper;

namespace CustomerApp.Shared.Services
{
    public class UserService
    {
        private readonly Client client;
        private readonly IMapper mapper;

        public UserService(Client client, IMapper mapper)
        {
            this.client = client;
            this.mapper = mapper;
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
            return client.Auth.CurrentUser != null;
        }

        public async Task Login(string email, string password)
        {
            var response = await client.Auth.SignIn(email, password);
            Console.WriteLine(response.ToString());
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
        }
    }
}
