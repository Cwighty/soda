﻿namespace CustomerApp.Features.Cart;

public partial class OrderProcessedPageViewModel : BaseViewModel
{
    public override Task Initialize()
    {
        return Task.CompletedTask;
    }

    public override Task Stop()
    {
        return Task.CompletedTask;
    }
}