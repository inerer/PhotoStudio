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
using PhotoStudio.Services.Interfaces;
using PhotoStudio.Views;
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
    private readonly SupplyRequestService _supplyRequestService;

    public MainPage(Worker worker)
    {
        _worker = worker;
        _request = new Request();
        _booking = new Booking();
        _requestService = new RequestService();
        _bookingService = new BookingService();
        _requestList = _requestService.Requests(_request);
        _bookingList = _bookingService.GetAllBookings(_booking);
        _supplyRequestService = new SupplyRequestService();
        
        InitializeComponent();
        Role();
        Delete();
        RenderWorker();
        RequestListBoxRendered();
        BookingListViewRendered();
    }

    private void RenderWorker()
    {
        // LastNameTextBlock.Text = _worker.PersonalInfo.LastName;
        // FirstNameTextBlock.Text = _worker.PersonalInfo.FirstName;
    }

    private void RequestListBoxRendered()
    {
        RequestListView.ItemsSource = null;
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
            var searchResult = _requestList
                .Where(r => r.Client.PersonalInfo.LastName.Contains(search));
            RequestListView.ItemsSource = null;
            RequestListView.ItemsSource = searchResult;
            var searchRentResult = _bookingList.Where(b => b.Request.Client.PersonalInfo.FullName.Contains(search));
            BookingListView.ItemsSource = null;
            BookingListView.ItemsSource = searchRentResult;
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
        BookingListView.ItemsSource = null;
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

    private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
    {
        var selectedItemRequest = (Request)RequestListView.SelectedItem;
        if(selectedItemRequest!=null)
            if (MessageBox.Show("Вы уверены, что хотите удалить?", "подтвердите удаление", MessageBoxButton.YesNo) ==
                MessageBoxResult.Yes)
            {
                _supplyRequestService.DeleteSupplyRequestByRequestId(selectedItemRequest);
                _bookingService.DeleteBookingByRequestId(selectedItemRequest);
                _requestService.DeleteRequest(selectedItemRequest.Id);
                Delete();
                RequestListBoxRendered();
                BookingListViewRendered();
            }

        var selectedItemBooking = (Booking)BookingListView.SelectedItem;
        if(selectedItemBooking!=null)
            if (MessageBox.Show("Вы уверены, что хотите удалить?", "подтвердите удаление", MessageBoxButton.YesNo) ==
                MessageBoxResult.Yes)
            {
                if (selectedItemBooking.Request != null)
                {
                    _bookingService.DeleteBookingByRequestId(selectedItemBooking.Request);
                    _requestService.DeleteRequest(selectedItemBooking.Request.Id);
                    Delete();
                    RequestListBoxRendered();
                    BookingListViewRendered();
                }
            }
    }

    private void AddNewSupply_OnClick(object sender, RoutedEventArgs e)
    {
        if (NavigationService != null) NavigationService.Navigate(new AddEditSupplyPage());
    }

    private void EditSupplyButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (NavigationService != null) NavigationService.Navigate(new AllSupplyPage());
    }

    private void SortForAlphabet_OnClick(object sender, RoutedEventArgs e)
    {
        List<Request> sortedList = new List<Request>(_requestList
            .OrderBy(r => r.Client?.PersonalInfo?.LastName));
        RequestListView.ItemsSource = null;
        RequestListView.ItemsSource = sortedList;
    }

    private void DatePicker_OnSelectedDateChanged(object? sender, SelectionChangedEventArgs e)
    {
        List<Request> filterList = new List<Request>();
        foreach (var req in _requestList)
        {
            if(req.RequestTimestamp != null && DatePicker.SelectedDate != null && DatePicker.SelectedDate.Value == req.RequestTimestamp.Value.Date)
                filterList.Add(req);
        }

        RequestListView.ItemsSource = null;
        RequestListView.ItemsSource = filterList;
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        RequestListBoxRendered();
    }

    private void DownRadioButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (RTabControl.SelectedItem == RequestItem)
        {
            var sortedList = new List<Request>(_requestList
                .OrderBy(r => r.Client?.PersonalInfo?.LastName));
            RequestListView.ItemsSource = null;
            RequestListView.ItemsSource = sortedList;
        }
        else
        {
            var sortedBookingList = new List<Booking>(_bookingList.OrderBy(b => b.Request.Client.PersonalInfo.FullName));
            BookingListView.ItemsSource = null;
            RequestListView.ItemsSource = sortedBookingList;
        }
    }

    private void UpRadioButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (RTabControl.SelectedItem == RequestItem)
        {
            var sortedList = new List<Request>(_requestList
                .OrderByDescending(r => r.Client?.PersonalInfo?.LastName));
            RequestListView.ItemsSource = null;
            RequestListView.ItemsSource = sortedList;

        }
        else
        {
            var sortedBookingList =
                new List<Booking>(_bookingList.OrderByDescending(b => b.Request.Client.PersonalInfo.FullName));
            BookingListView.ItemsSource = null;
            BookingListView.ItemsSource = sortedBookingList;
        }
       
        
    }

    private void RegistrationPage_OnClick(object sender, RoutedEventArgs e)
    {
        if (NavigationService != null) NavigationService.Navigate(new RegistrPage());
    }

    private void RegistrBlock()
    {
        if (_worker.Role.Id != 1)
            RegistrationButton.Visibility = Visibility.Collapsed;
        else
            RegistrationButton.Visibility = Visibility.Visible;
    }

    private void ClientPage_OnClick(object sender, RoutedEventArgs e)
    {
        if (NavigationService != null) NavigationService.Navigate(new ClientInfoPage());
    }

    private void Role()
    {
        if (_worker.Role != null && _worker.Role.Id != 1)
        {
            AddNewSupplyButton.Visibility = Visibility.Collapsed;
            EditSupplyButton.Visibility = Visibility.Collapsed;
            RegistrationButton.Visibility = Visibility.Collapsed;
        }
    }
}