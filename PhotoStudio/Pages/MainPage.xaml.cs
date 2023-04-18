using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ModernWpf.Controls;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Models.DataBase.SupplyRequestModels;
using PhotoStudio.Services;
using PhotoStudio.Views;
using PhotoStudio.Windows;
using Page = System.Windows.Controls.Page;

namespace PhotoStudio.Pages;

public partial class MainPage : Page
{
    private readonly Worker _worker;
    private readonly Request _request;
    private readonly RequestService _requestService;
    private readonly Booking _booking;
    private readonly BookingService _bookingService;
    private readonly List<Request> _requestList;
    private readonly List<Booking> _bookingList;

    public MainPage(Worker worker)
    {
        _worker = worker;
        _request = new Request();
        _booking = new Booking();
        _requestService = new RequestService();
        _bookingService = new BookingService();
        _requestList = _requestService.Requests(_request);
        _bookingList = _bookingService.GetAllBookings(_booking);
        InitializeComponent();
        Delete();
        RenderWorker();
        RequestListBoxRendered();
        BookingListViewRendered();
    }

    private void RenderWorker()
    {
        LastNameTextBlock.Text = _worker.PersonalInfo.LastName;
        FirstNameTextBlock.Text = _worker.PersonalInfo.FirstName;
    }

    private void RequestListBoxRendered()
    {
        RequestListView.ItemsSource = _requestList;
    }

    private void RequestListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = (Request)RequestListView.SelectedItem;
        ShowFullRequestInfoDialog(selectedItem, _worker);
    }

    private async Task ShowFullRequestInfoDialog(Request request, Worker worker)
    {
        ContentDialog contentDialog = new ContentDialog
        {
            Title = request.Client.PersonalInfo.FullName,
            Content = new FullRequestView(request, worker),
            CloseButtonText = "Закрыть"
        };

        await contentDialog.ShowAsync();
    }

    private void SearchTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        var search = SearchTextBox.Text;
        if (search != String.Empty)
        {
            var searchResult = _requestService.Requests(_request)
                .Where(r => r.Client.PersonalInfo.LastName.Contains(search));
            RequestListView.ItemsSource = null;
            RequestListView.ItemsSource = searchResult;
        }
        else
        {
            RequestListBoxRendered();
        }
    }

    private void BookingListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = (Booking)BookingListView.SelectedItem;
        ShowBookingFullInfoDialog(selectedItem);

    }

    private void BookingListViewRendered()
    {
       BookingListView.ItemsSource = _bookingList;
    }

    private void Delete()
    {
        _requestList.RemoveAll(el => _bookingList.Exists(el2 => el2.Request.Id == el.Id));
    }

    private async Task ShowBookingFullInfoDialog(Booking booking)
    {
        ContentDialog contentDialog = new ContentDialog
        {
            Title = "Заказ",
            Content = new BookingFullInfo(booking),
            CloseButtonText = "Закрыть"
        };
        await contentDialog.ShowAsync();
    }
}