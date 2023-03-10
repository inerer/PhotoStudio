using System.Windows.Controls;
using System.Windows.Input;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Services;
using PhotoStudio.Windows;

namespace PhotoStudio.Pages;

public partial class MainPage : Page
{
    private readonly Worker _worker;
    private readonly Request _request;
    private readonly RequestService _requestService;

    public MainPage(Worker worker)
    {
        _worker = worker;
        _request = new Request();
        _requestService = new RequestService();
        
        InitializeComponent();
        RenderWorker();
        RequestListBoxRendered();
    }

    private void RenderWorker()
    {
        LastNameTextBlock.Text = _worker.PersonalInfo.LastName;
        FirstNameTextBlock.Text = _worker.PersonalInfo.FirstName;
    }

    private void RequestListBoxRendered()
    {
        RequestListView.ItemsSource = _requestService.Requests(_request);
    }

    private void RequestListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = (Request)RequestListView.SelectedItem;
        FullRequestInfoWindow fullRequestInfoWindow = new FullRequestInfoWindow(selectedItem);
        fullRequestInfoWindow.ShowDialog();
        //NavigationService.Navigate(new FullInfo(selectedItem));

    }
}