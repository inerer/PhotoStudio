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
    private PersonalInfoService _personalInfoService;
    private readonly List<Supply> _supplyList;
    private readonly List<Supply> _rentList;

    public ClientPage(Client client)
    {
        InitializeComponent();
        _supplyService = new SupplyService();
        _supply = new Supply();
        _supplies = new List<Supply>();
        _client = client;
        _rentList = new List<Supply>();
        _supplyList = new List<Supply>();
        AllListViewsRendered();
    }

    private void AllListViewsRendered()
    {
        foreach (var item in _supplyService.GetAllSupplies(_supply))
        {
            if(item.TypeSupply.Id==1)
                _rentList.Add(item);
            else
                _supplyList.Add(item);
        }

        SupplyListView.ItemsSource = _supplyList;
        RentListView.ItemsSource = _rentList;
    }

    private void RentListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = (Supply)SupplyListView.SelectedItem;
    }

    private void AddCartButton_OnClick(object sender, RoutedEventArgs e)
    {
        var selectedItem = (Supply)SupplyListView.SelectedItem;
        _supplies.Add(selectedItem);
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

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        ShowCartDialog(_supplies, _client);
    }
    
}