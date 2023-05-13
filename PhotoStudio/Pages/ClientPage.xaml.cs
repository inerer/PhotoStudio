using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ModernWpf.Controls;
using PhotoStudio.DataBase.Repositories;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Models.DataBase.SupplyRequestModels;
using PhotoStudio.Services;
using PhotoStudio.Views;
using Page = System.Windows.Controls.Page;

namespace PhotoStudio.Pages;

public partial class ClientPage : Page
{
    private readonly SupplyService _supplyService;
    private readonly Supply _supply;
    private readonly List<Supply> _supplies;
    private readonly Client _client;
    public ClientPage(Client client)
    {
        InitializeComponent();
        _supplyService = new SupplyService();
        _supply = new Supply();
        _supplies = new List<Supply>();
        _client = client;
        AllListViewsRendered();
    }

    private void AllListViewsRendered()
    {
        SupplyListView.ItemsSource = _supplyService.GetAllSuppliesDontRent(_supply);
        RentListView.ItemsSource = _supplyService.GetAllSupplies(_supply);
    }

    private void RentListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = (Supply)SupplyListView.SelectedItem;
       
    }

    private void AddCartButton_OnClick(object sender, RoutedEventArgs e)
    {
        var selectedItem = (Supply)SupplyListView.SelectedItem;
        var selectedItemRent = (Supply)RentListView.SelectedItem;
        
        if (selectedItem != null)
            _supplies.Add(selectedItem);

        if (selectedItemRent != null)
            _supplies.Add(selectedItemRent);
        
        MessageBox.Show("Услуга добавлена");
    }

    private async Task ShowCartDialog(List<Supply> supplies, Client client)
    {
        ContentDialog contentDialog = new ContentDialog
        {
            Title = "Корзина",
            Content = new CartView(supplies, client),
            CloseButtonText = "Закрыть корзину",
        };
        await contentDialog.ShowAsync();
    }

    private async Task ShowPhotoClientDialog(Client client)
    {
        ContentDialog contentDialog = new ContentDialog
        {
            Title = "Фото",
            Content = new PhotoClientView(client),
            CloseButtonText = "Закрыть"
        };
        await contentDialog.ShowAsync();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        ShowCartDialog(_supplies, _client);
    }

    private void PhotoButton_OnClick(object sender, RoutedEventArgs e)
    {
        ShowPhotoClientDialog(_client);
    }
}