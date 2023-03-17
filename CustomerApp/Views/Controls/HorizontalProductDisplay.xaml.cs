using CommunityToolkit.Maui.Behaviors;
using System.Windows.Input;

namespace CustomerApp.Views.Controls;

public partial class HorizontalProductDisplay : ContentView
{
    // Bindable properties have to be static to work
    //The bindable property must have the same name as its propery except for appending 'property'
    public static BindableProperty ProductsProperty = BindableProperty.Create(nameof(Products), typeof(List<ProductData>), typeof(HorizontalProductDisplay), null, propertyChanged: OnProductsChanged);
    public static BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(HorizontalProductDisplay), null, propertyChanged: OnTitleChanged);
    public static BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(HorizontalProductDisplay), null, propertyChanged: CommandChanged);
    public static BindableProperty ShowAllEnabledProperty = BindableProperty.Create(nameof(ShowAllEnabled), typeof(bool), typeof(HorizontalProductDisplay), true, propertyChanged: OnSeeAllEnabledChanged);

    public List<ProductData> Products
    {
        get => (List<ProductData>)GetValue(ProductsProperty);
        set =>SetValue(ProductsProperty, value);
    }
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public HorizontalProductDisplay()
    {
        InitializeComponent();
    }


    private static void OnProductsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (HorizontalProductDisplay)bindable;
        control.ProductsCollection.ItemsSource = (List<ProductData>)newValue;
    }
    private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (HorizontalProductDisplay)bindable;
        control.SectionLabel.Text = (string)newValue;
    }
    private static void CommandChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (HorizontalProductDisplay)bindable;
        control.ShowAllLabel.GestureRecognizers.Add(new TapGestureRecognizer()
        {
            Command = (ICommand)newValue,
            CommandParameter = control.Products,
        });
        control.ShowallButton.Command = (ICommand)newValue;
        control.ShowallButton.CommandParameter = control.Products;
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