using System;
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
    private readonly RentService _rentService;
    public AddEditSupplyPage()
    {
        InitializeComponent();
        _typeSupply = new TypeSupply();
        _typeSupplyService = new TypeSupplyService();
        _supply = new Supply();
        _rent = new Rent();
        _hall = new Hall();
        _supplyService = new SupplyService();
        _rentService = new RentService();
        this.DataContext = _supply;
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
        

        RentComboBox.ItemsSource = _rentService.GetAllRents(_rent);

    }

    private void TypeSupplyComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _supply.TypeSupply = (TypeSupply)TypeSupplyComboBox.SelectedItem;

        if (_supply.TypeSupply.Id == 1)
        {
            RentComboBox.Visibility = Visibility.Visible;
            
        }
           
    }
    
    private void AddTypeSupplyButton_OnClick(object sender, RoutedEventArgs e)
    {
       
    }

    private void AddRentButton_OnClick(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void AddButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            if (_supply.TypeSupply is { Id: 1 })
                _supplyService.AddSupplyForRent(_supply);
            else
                _supplyService.AddSupply(_supply);

            MessageBox.Show("Услуга успешно добавлена");
        }
        catch
        {

            MessageBox.Show("Ошибка добавления услуги");
        }
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }
}