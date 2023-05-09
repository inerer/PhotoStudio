using System;
using System.Windows;
using System.Windows.Controls;
using PhotoStudio.Models.DataBase.SupplyRequestModels;
using PhotoStudio.Services;

namespace PhotoStudio.Views;

public partial class EditSupply : UserControl
{
    private readonly TypeSupply _typeSupply;
    private readonly TypeSupplyService _typeSupplyService;
    private readonly Supply _supply;
    private readonly Rent _rent;
    private readonly RentService _rentService;
    private readonly SupplyService _supplyService;
    public EditSupply(Supply supply)
    {
        InitializeComponent();
        _supply = supply;
        this.DataContext = _supply;
        _typeSupply = new TypeSupply();
        _typeSupplyService = new TypeSupplyService();
        _rentService = new RentService();
        _rent = new Rent();
        _supplyService = new SupplyService();
    }
    private void AddButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            _supplyService.EditSupply(_supply);
            MessageBox.Show("Изменение прошло успешно");
        }
        catch (Exception exception)
        {
            MessageBox.Show("Ошибка изменения");
        }
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
    }

    private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            _supplyService.DeleteSupply(_supply.Id);
            MessageBox.Show("Удаление прошло успешно");
        }
        catch (Exception exception)
        {
            MessageBox.Show("Ошибка при удалении");
        }
    }
}