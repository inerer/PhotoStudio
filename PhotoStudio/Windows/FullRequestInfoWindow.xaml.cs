using System.Windows;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Services.Interfaces;

namespace PhotoStudio.Windows;

public partial class FullRequestInfoWindow : Window
{
    private readonly Request _request;
    private readonly SupplyRequestService _supplyRequestService;
    public FullRequestInfoWindow(Request newRequest)
    {
        _request = newRequest;
        _supplyRequestService = new SupplyRequestService();
        InitializeComponent();
        RenderServiceRequestInfo();
    }

    private void RenderServiceRequestInfo()
    {
        FullInfoListView.ItemsSource = _supplyRequestService.GelSupplyRequestByRequestId(_request);
    }
}