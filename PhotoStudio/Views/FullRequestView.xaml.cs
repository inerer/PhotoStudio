using System.Windows.Controls;
using PhotoStudio.Models.DataBase.SupplyRequestModels;
using PhotoStudio.Services.Interfaces;

namespace PhotoStudio.Views;

public partial class FullRequestView : UserControl
{
    public FullRequestView(Request request)
    {
        _request = request;
        _supplyRequestService = new SupplyRequestService();
        InitializeComponent();
        RenderServiceRequestInfo();
    }    
    private readonly Request _request;
    private readonly SupplyRequestService _supplyRequestService;

    private void RenderServiceRequestInfo()
    {
        FullInfoListView.ItemsSource = _supplyRequestService.GelSupplyRequestByRequestId(_request);
    }
}