using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Mvvm.ComponentModel;
using CustomerApp.Models;
using System.Runtime.CompilerServices;

namespace CustomerApp.Views.Controls;

public partial class HorizontalProductDisplay : ContentView
{
    // Bindable properties have to be static to work
    //The bindable property must have the same name as its propery except for appending 'property'
    public static BindableProperty ProductsProperty = BindableProperty.Create(nameof(Products), typeof(List<ProductData>), typeof(HorizontalProductDisplay), propertyChanged: OnProductsChanged);
    
    private static void OnProductsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (HorizontalProductDisplay)bindable;
        control.ProductsCollection.ItemsSource = (List<ProductData>) newValue;
    }

    public List<ProductData> Products
    {
        get => (List<ProductData>)GetValue(ProductsProperty);
        set =>SetValue(ProductsProperty, value);
    }

    public static BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(HorizontalProductDisplay), propertyChanged: OnTitleChanged);

    private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (HorizontalProductDisplay) bindable;
        control.SectionLabel.Text = (string)newValue;
    }

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    
    public static BindableProperty ShowAllCommandProperty = BindableProperty.Create(nameof(ShowAllCommand), typeof(Command), typeof(HorizontalProductDisplay), propertyChanged: OnSeeAllCommandChanged);

    private static void OnSeeAllCommandChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (HorizontalProductDisplay)bindable;
        control.ProductListHeader.Behaviors.Add(new EventToCommandBehavior {
            EventName = "Tapped",
            Command = (Command)newValue
        });
    }

    public Command ShowAllCommand
    {
        get => (Command)GetValue(ShowAllCommandProperty);
        set => SetValue(ShowAllCommandProperty, value);
    }

    public static BindableProperty ShowAllEnabledProperty = BindableProperty.Create(nameof(ShowAllEnabled), typeof(bool), typeof(HorizontalProductDisplay), true, propertyChanged: OnSeeAllEnabledChanged);

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

    public HorizontalProductDisplay()
    {
        InitializeComponent();
    }
}