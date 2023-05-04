using System.Windows;
using System.Windows.Controls;
using PhotoStudio.Models.DataBase.SupplyRequestModels;
using PhotoStudio.Services;

namespace PhotoStudio.Pages;

public partial class AddEditSupplyPage : Page
{
    private readonly TypeSupply _typeSupply;
    private readonly TypeSupplyService _typeSupplyService;
    private readonly Supply _supply;
    private readonly Rent _rent;
    private readonly Hall _hall;
    private readonly SupplyService _supplyService;
    public AddEditSupplyPage()
    {
        InitializeComponent();
        _typeSupply = new TypeSupply();
        _typeSupplyService = new TypeSupplyService();
        _supply = new Supply();
        _rent = new Rent();
        _hall = new Hall();
        _supplyService = new SupplyService();
        ComboBoxRendered();
    }

    private void RentComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _supply.Rent = (Rent)RentComboBox.SelectedItem;
    }

    private void ComboBoxRendered()
    {
        TypeSupplyComboBox.ItemsSource = _typeSupplyService.GetAllTypeSupplies(_typeSupply);
        
        RentComboBox.Visibility = Visibility.Collapsed;
        AddRentButton.Visibility = Visibility.Collapsed;

        RentComboBox.ItemsSource = _supplyService.GetAllSupplies(_supply);

    }

    private void TypeSupplyComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _supply.TypeSupply = (TypeSupply)TypeSupplyComboBox.SelectedItem;

        if (_supply.TypeSupply.Id == 1)
        {
            RentComboBox.Visibility = Visibility.Visible;
            AddRentButton.Visibility = Visibility.Visible;
        }
           
    }

    private void AddTypeSupplyButton_OnClick(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void AddRentButton_OnClick(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }
}