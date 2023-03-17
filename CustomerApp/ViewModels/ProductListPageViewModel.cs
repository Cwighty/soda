﻿

namespace CustomerApp.ViewModels;

[INotifyPropertyChanged]
[QueryProperty(nameof(Products), nameof(Products))]
public partial class ProductListPageViewModel : BaseViewModel
{
    [ObservableProperty]
    private List<ProductData> products;
    public ProductListPageViewModel(ProductService productService)
    {

    }
    public override Task Initialize()
    {
        return Task.CompletedTask;
    }

    public override Task Stop()
    {
        return Task.CompletedTask;
    }
}
