using System;
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
        ShowFullRequestInfoDialog(selectedItem);
    }

    private async Task ShowFullRequestInfoDialog(Request request)
    {
        ContentDialog contentDialog = new ContentDialog
        {
            Title = request.Client.PersonalInfo.FullName,
            Content = new FullRequestView(request),
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
}