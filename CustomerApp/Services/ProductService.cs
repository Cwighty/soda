using CustomerApp.Models;
using Supabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApp.Services;

public class ProductService
{
    private readonly Client client;

    public ProductService(Client client)
	{
        this.client = client;
    }

    public async Task<List<ProductData>> GetProducts()
    {
        var response = await client.From<ProductData>().Get();
        return response.Models;
    }
}
