using CustomerApp.Models;
using Supabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApp.Services;

public class PurchaseService
{
    private readonly Client client;

    public PurchaseService(Client client)
    {
        this.client = client;
    }

    public async Task<List<PurchaseData>> GetPurchases()
    {
        var response = await client.From<PurchaseData>().Get();
        return response.Models;
    }
}
