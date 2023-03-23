using Client = Supabase.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace CustomerApp.Services
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
    }
}
