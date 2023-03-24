using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using MahApps.Metro.Controls;
using ModernWpf.Controls;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Models.DataBase.SupplyRequestModels;
using PhotoStudio.Services;
using PhotoStudio.Services.Interfaces;

namespace PhotoStudio.Views;

public partial class CartView : UserControl
{
    private readonly List<Supply> _supplies;
    private readonly List<Request> _requests;
    private readonly Request _request;
    private readonly RequestService _requestService;
    private readonly SupplyRequestService _supplyRequestService;
    private readonly SupplyRequest _supplyRequest;
    public CartView(List<Supply> supplies , Client client)
    {
        _supplies = supplies;
        _requestService = new RequestService();
        _request = new Request
        {
            Client = client
        };
        _supplyRequest = new SupplyRequest
        {
            Request = _request
        };
        InitializeComponent();
        RenderCartListView();
    }

    private void DeleteCartElementButton_OnClick(object sender, RoutedEventArgs e)
    {
        DeleteElementCartList();
    }

    private void DesignCartButton_OnClick(object sender, RoutedEventArgs e)
    {
        AddServiceRequest();
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

    private void AddServiceRequest()
    {
        _requestService.AddRequest(_request);
        foreach (var item in _supplies)
        {
            _supplyRequest.Supply = item;
            _supplyRequestService.AddSupplyRequest(_supplyRequest);
        }
    }
}