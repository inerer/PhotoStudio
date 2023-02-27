using System.Windows.Controls;
using System.Windows.Input;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Services;

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
        var a = RequestListView.SelectedItem;
    }


    private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void RequestListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        throw new System.NotImplementedException();
    }
}