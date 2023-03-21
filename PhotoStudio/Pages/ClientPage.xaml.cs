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

    public ClientPage()
    {
        InitializeComponent();
        _supplyService = new SupplyService();
        _supply = new Supply();
        _supplies = new List<Supply>();
        SupplyListViewRendered();
        RentListViewRendered();

    }

    private void SupplyListViewRendered()
    {
        SupplyListView.ItemsSource = _supplyService.GetAllSupplies(_supply);
    }

    private void RentListViewRendered()
    {
        
        RentListView.ItemsSource = _supplyService.GetAllSupplies(_supply);
    }

    private void RentListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = (Supply)SupplyListView.SelectedItem;
        if (selectedItem.TypeSupply.Id == 2)
        {
            
        }
        else
            MessageBox.Show("В попу иди");
    }

    private void AddCartButton_OnClick(object sender, RoutedEventArgs e)
    {
        var selectedItem = (Supply)SupplyListView.SelectedItem;
        _supplies.Add(selectedItem);
    }

    private async Task ShowCartDialog(List<Supply> supplies)
    {
        ContentDialog contentDialog = new ContentDialog
        {
            Title = "Корзина",
            Content = new CartView(supplies),
            CloseButtonText = "Закрыть корзину"
        };
        await contentDialog.ShowAsync();
        
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        ShowCartDialog(_supplies);
    }
}