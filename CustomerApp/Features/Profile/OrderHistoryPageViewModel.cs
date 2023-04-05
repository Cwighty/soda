﻿namespace CustomerApp.Features.Profile;

public partial class OrderHistoryPageViewModel : BaseViewModel
{
    private readonly OrderService orderService;

    [ObservableProperty]
    private List<Purchase> purchases;

    public OrderHistoryPageViewModel(OrderService orderService)
    {
        this.orderService = orderService;
    }
    public async override Task Initialize()
    {
        Purchases = await orderService.GetPurchaseHistory();
    }

    public override Task Stop()
    {
        return Task.CompletedTask;
    }
}
