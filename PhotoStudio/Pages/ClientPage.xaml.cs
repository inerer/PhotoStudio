using System.Windows.Controls;
using PhotoStudio.DataBase.Repositories;
using PhotoStudio.Models.DataBase;
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
        ListViewRendered();

    }

    private void ListViewRendered()
    {
        SupplyListView.ItemsSource = _supplyService.GetAllSupplies(_supply);
    }
}