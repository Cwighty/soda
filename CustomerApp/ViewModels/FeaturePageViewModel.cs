﻿using CommunityToolkit.Mvvm.Input;

namespace CustomerApp.ViewModels;

[INotifyPropertyChanged]
public partial class FeaturePageViewModel : BaseViewModel
{
    [ObservableProperty] 
    private List<CategoryData> _categorizedProducts;

    private readonly ProductService productService;
    private readonly NavigationService navigationService;

    public FeaturePageViewModel(ProductService productService, NavigationService navigationService)
    {
        this.productService = productService;
        this.navigationService = navigationService;
    }
    public override async Task Initialize()
    {
        var categoryProducts = await productService.GetCategorizedProducts();
        CategorizedProducts = categoryProducts;
    }

    public override Task Stop()
    {
        return Task.CompletedTask;
    }

    [RelayCommand]
    private async Task NavigateToDetail(ProductData product)
    {
        await navigationService
            .GoTo(
                nameof(ProductDetailPage),
                new Dictionary<string, object>()
                {
                    ["Product"] = product
                });
    }
}
