using AutoMapper;
using SodaShared.Models.Data;
using SodaShared.Models;
using Supabase;

namespace StoreApp.Services;

public class PurchaseService
{
    private readonly Client client;
    private readonly IMapper mapper;

    public PurchaseService(Client client, IMapper mapper)
    {
        this.client = client;
        this.mapper = mapper;
    }
    public async Task<int> PersistPurchase(Purchase purchase)
    {
        var newPurchaseData = mapper.Map<PurchaseData>(purchase);
        var newPurchase = await client.From<PurchaseData>()
            .Insert(newPurchaseData, new Postgrest.QueryOptions { Returning = Postgrest.QueryOptions.ReturnType.Representation });

        var purchaseId = newPurchase.Models.FirstOrDefault()!.Id;

        await PersistPurchaseItems(purchaseId, purchase.PurchaseItems);
        return purchaseId;
    }

    public async Task PersistPurchaseItems(int purchaseId, List<PurchaseItem> purchaseItems)
    {
        var sizes = (await client.From<SizeData>().Get()).Models;
        foreach (var item in purchaseItems)
        {
            var newPurchaseItemData = mapper.Map<PurchaseItemData>(item);
            newPurchaseItemData.PurchaseId = purchaseId;
            newPurchaseItemData.SizeId = sizes.Where(s => s.Name == item.Size.Name).FirstOrDefault().Id;
            var newItem = await client.From<PurchaseItemData>()
                .Insert(newPurchaseItemData, new Postgrest.QueryOptions { Returning = Postgrest.QueryOptions.ReturnType.Representation });

            var purchaseItemId = newItem.Models.FirstOrDefault()!.Id;

            await PersistPurchaseItemAddons(purchaseItemId, item.AddOns);
        }
    }

    public async Task PersistPurchaseItemAddons(int itemId, List<AddOn> addons)
    {
        foreach (var addon in addons)
        {
            var newAddon = new PurchaseItemAddOnData
            {
                PurchaseItemId = itemId,
                AddOnId = addon.Id
            };
            await client.From<PurchaseItemAddOnData>()
                .Insert(newAddon);
        }
    }

    public async Task<SizeData> LookUpSize(int id)
    {
        return await client.From<SizeData>().Where(s => s.Id == id).Single();
    }

    public async Task<BaseData> LookUpBase(int id)
    {
        return await client.From<BaseData>().Where(b => b.Id == id).Single();
    }

    public async Task<AddOnData> LookUpAddon(int id)
    {
        return await client.From<AddOnData>().Where(a => a.Id == id).Single();
    }
}
