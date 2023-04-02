using System.Windows.Controls;
using PhotoStudio.Models.DataBase.SupplyRequestModels;
using PhotoStudio.Services.Interfaces;

namespace PhotoStudio.Views;

public partial class FullRequestView : UserControl
{
    private readonly Request _request;
    private readonly SupplyRequestService _supplyRequestService;
    public FullRequestView(Request request)
    {
        _request = request;
        _supplyRequestService = new SupplyRequestService();
        InitializeComponent();
        RenderServiceRequestInfo();
        RenderTotalPriceLabel();
    }    
    
    private void RenderServiceRequestInfo()
    {
        FullInfoListView.ItemsSource = _supplyRequestService.GelSupplyRequestByRequestId(_request);
    }

    private void RenderTotalPriceLabel()
    {
        decimal totalPrice = 0;
        
        foreach (var supplyRequest in _supplyRequestService.GelSupplyRequestByRequestId(_request))
        {
           totalPrice += (decimal)supplyRequest.Supply.Price;
        }

        TotalPriceLabel.Content = totalPrice;
    }
}