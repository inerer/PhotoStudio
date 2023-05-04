using System.Windows;
using System.Windows.Controls;
using PhotoStudio.Models.DataBase.SupplyRequestModels;
using PhotoStudio.Services;

namespace PhotoStudio.Pages;

public partial class AddEditSupplyPage : Page
{
    private TypeSupply _typeSupply;
    private TypeSupplyService _typeSupplyService;
    public AddEditSupplyPage()
    {
        InitializeComponent();
       
        _typeSupply = new TypeSupply();
        _typeSupplyService = new TypeSupplyService();
        ComboBoxRendered();
    }

    private void DescriptionSupplyTextBox_OnSelectionChanged(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void RentComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void ComboBoxRendered()
    {
        TypeSupplyComboBox.ItemsSource = _typeSupplyService.GetAllTypeSupplies(_typeSupply);
    }
}