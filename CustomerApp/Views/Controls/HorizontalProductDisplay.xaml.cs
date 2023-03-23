using CommunityToolkit.Maui.Behaviors;
using System.Windows.Input;

namespace CustomerApp.Views.Controls;

public partial class HorizontalProductDisplay : ContentView
{
    // Bindable properties have to be static to work
    //The bindable property must have the same name as its propery except for appending 'property'
    public static BindableProperty ProductsProperty = BindableProperty.Create(nameof(Products), typeof(List<Product>), typeof(HorizontalProductDisplay), null, propertyChanged: OnProductsChanged);
    public static BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(HorizontalProductDisplay), null, propertyChanged: OnTitleChanged);
    public static BindableProperty ShowAllCommandProperty = BindableProperty.Create(nameof(ShowAllCommand), typeof(ICommand), typeof(HorizontalProductDisplay), null, propertyChanged: ShowAllCommandChanged);
    public static BindableProperty ShowAllEnabledProperty = BindableProperty.Create(nameof(ShowAllEnabled), typeof(bool), typeof(HorizontalProductDisplay), true, propertyChanged: OnSeeAllEnabledChanged);
    public static BindableProperty DetailsCommandProperty = BindableProperty.Create(nameof(DetailsCommand), typeof(ICommand), typeof(HorizontalProductDisplay), null);

  

    public List<Product> Products
    {
        get => (List<Product>)GetValue(ProductsProperty);
        set =>SetValue(ProductsProperty, value);
    }
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    public ICommand ShowAllCommand
    {
        get => (ICommand)GetValue(ShowAllCommandProperty);
        set => SetValue(ShowAllCommandProperty, value);
    }
    
    public ICommand DetailsCommand
    {
        get => (ICommand)GetValue(DetailsCommandProperty);
        set => SetValue(DetailsCommandProperty, value);
    }

    public HorizontalProductDisplay()
    {
        InitializeComponent();
    }


    private static void OnProductsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (HorizontalProductDisplay)bindable;
        control.ProductsCollection.ItemsSource = (List<Product>)newValue;
    }
    private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (HorizontalProductDisplay)bindable;
        control.SectionLabel.Text = (string)newValue;
    }
    private static void ShowAllCommandChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (HorizontalProductDisplay)bindable;
        control.ShowAllLabel.GestureRecognizers.Add(new TapGestureRecognizer()
        {
            Command = (ICommand)newValue,
            CommandParameter = control.Products,
        });
    }
    
    private static void OnSeeAllEnabledChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (HorizontalProductDisplay)bindable;
        control.ShowAllLabel.IsVisible = (bool)newValue;
    }
    public bool ShowAllEnabled
    {
        get => (bool)GetValue(ShowAllEnabledProperty);
        set => SetValue(ShowAllEnabledProperty, value);
    }
}