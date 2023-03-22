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
    private  Client _client;
    private PersonalInfoService _personalInfoService;

    public ClientPage(Client client)
    {
        InitializeComponent();
        _supplyService = new SupplyService();
        _supply = new Supply();
        _supplies = new List<Supply>();
        _client = client;
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

    private async Task ShowCartDialog(List<Supply> supplies, Client client)
    {
        ContentDialog contentDialog = new ContentDialog
        {
            Title = "Корзина",
            Content = new CartView(supplies, client),
            CloseButtonText = "Закрыть корзину",
        };
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        ShowCartDialog(_supplies);
    }
    
   //private async Task ShowClientNameDialog()
   //{
   //    _client = new Client();
   //    ContentDialog contentDialog = new ContentDialog
   //    {
   //        Title = "Введите ваши данны",
   //        Content = new ClientNameView(_client),
   //        CloseButtonText = "Отменить оформление",
   //        PrimaryButtonText = "Зарегистрировать"
   //        
   //    };
   //   var result =  await contentDialog.ShowAsync();
   //   if (result == ContentDialogResult.Primary) 
   //       if (!string.IsNullOrWhiteSpace(_client.PersonalInfo.FirstName)) 
   //       { 
   //           var commandResult = await _recipeService.AddCategoryAsync(category); 
   //           _categories.Add(category); 
   //           Categories = new ObservableCollection<Category>(_categories); 
   //       } 
   //}
}