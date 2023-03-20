using System.Windows;
using System.Windows.Controls;
using PhotoStudio.DataBase.Repositories;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Models.DataBase.SupplyRequestModels;
using PhotoStudio.Services;

namespace PhotoStudio.Pages;

public partial class ClientPage : Page
{
    private readonly SupplyService _supplyService;
    private readonly Supply _supply;
    
    public ClientPage()
    {
        InitializeComponent();
        _supplyService = new SupplyService();
        _supply = new Supply();
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
}