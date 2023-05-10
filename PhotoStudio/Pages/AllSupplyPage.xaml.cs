using System.Threading.Tasks;
using System.Windows.Controls;
using ModernWpf.Controls;
using PhotoStudio.Models.DataBase.SupplyRequestModels;
using PhotoStudio.Services;
using PhotoStudio.Views;
using Page = System.Windows.Controls.Page;

namespace PhotoStudio.Pages;

public partial class AllSupplyPage : Page
{
    private readonly SupplyService _supplyService;
    private readonly Supply _supply;
    
    public AllSupplyPage()
    {
        _supplyService = new SupplyService();
        _supply = new Supply();
        
        InitializeComponent();
        
        ListViewRendered();
    }

    private void ListViewRendered()
    {
        SupplyListView.ItemsSource = null;
        SupplyListView.ItemsSource = _supplyService.GetAllSuppliesDontRent(_supply);
        RentListView.ItemsSource = null;
        RentListView.ItemsSource = _supplyService.GetAllSupplies(_supply);
    }

    private async Task ShowEditSupplyDialog(Supply supply)
    {
        ContentDialog contentDialog = new ContentDialog
        {
            Title = "Изменение услуги",
            Content = new EditSupply(supply),
            CloseButtonText = "Закрыть"
        };
        await contentDialog.ShowAsync();
        ListViewRendered();
    }

    private async void RentListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      await ShowEditSupplyDialog((Supply)RentListView.SelectedItem);
        
        
    }

    private async void SupplyListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
       await ShowEditSupplyDialog((Supply)SupplyListView.SelectedItem);
        
    }
}