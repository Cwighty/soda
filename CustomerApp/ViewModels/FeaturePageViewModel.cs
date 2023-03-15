using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CustomerApp.Models;
using CustomerApp.Services;
using CustomerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApp.ViewModels
{
    [INotifyPropertyChanged]
    public partial class FeaturePageViewModel : BaseViewModel
    {
        [ObservableProperty] 
        private List<CategoryData> _categorizedProducts;

        private readonly ProductService productService;

        public FeaturePageViewModel(ProductService productService)
        {
            this.productService = productService;
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
    }
}
