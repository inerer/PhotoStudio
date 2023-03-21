using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using MahApps.Metro.Controls;
using ModernWpf.Controls;
using PhotoStudio.Models.DataBase.SupplyRequestModels;

namespace PhotoStudio.Views;

public partial class CartView : UserControl
{
    private readonly List<Supply> _supplies;
    public CartView(List<Supply> supplies)
    {
        _supplies = supplies;
        InitializeComponent();
        RenderCartListView();
    }

    private void DeleteCartElementButton_OnClick(object sender, RoutedEventArgs e)
    {
        DeleteElementCartList();
    }

    private void DesignCartButton_OnClick(object sender, RoutedEventArgs e)
    {
        
    }

    private void RenderCartListView()
    {
        CartListView.ItemsSource = null;
        CartListView.ItemsSource = _supplies;
    }

    private void DeleteElementCartList()
    {
        var selectedItem = (Supply)CartListView.SelectedItem;
        _supplies.Remove(selectedItem);
        RenderCartListView();
    }

    private async Task ShowClientNameDialog()
    {
        ContentDialog contentDialog = new ContentDialog
        {
            Title = "Введите ваши данны",
            Content = new ClientNameView(),
            CloseButtonText = "Отменить оформление"
        };
        await contentDialog.ShowAsync();
    }
}