using System.Windows.Controls;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Models.DataBase.SupplyRequestModels;
using PhotoStudio.Services.Interfaces;

namespace PhotoStudio.Views;

public partial class BookingFullInfo : UserControl
{

    private readonly Booking _booking;
    private readonly SupplyRequest _supplyRequest;
    private Request _request;
    private readonly SupplyRequestService _supplyRequestService;
    public BookingFullInfo(Booking booking)
    {
        _request = new Request();
        _booking = booking;
        _supplyRequest = new SupplyRequest();
        _supplyRequestService = new SupplyRequestService();
        InitializeComponent();
        RenderListView();
    }

    private void RenderListView()
    {
        if (_booking.Request != null) _request = _booking.Request;
        FullInfoBookingListView.ItemsSource = _supplyRequestService.GelSupplyRequestByRequestId(_request);
    }
}