using System.Windows;
using System.Windows.Controls;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Services;

namespace PhotoStudio.Pages;

public partial class ClientInfoPage : Page
{
    private readonly PersonalInfo _personalInfo;
    private readonly PersonalInfoService _personalInfoService;
    private readonly Client _client;
    private readonly ClientService _clientService;
    
    public ClientInfoPage()
    {
        InitializeComponent();
        _personalInfo = new();
        _client = new();
        this.DataContext = _personalInfo;
        _personalInfoService = new();
        _clientService = new ClientService();
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new AuthPage());
    }

    private void SubmitButton_OnClick(object sender, RoutedEventArgs e)
    {
        //try
        //{
            _client.PersonalInfo =
                _personalInfoService.GetPersonalInfo(_personalInfoService.AddPersonalInfo((PersonalInfo)DataContext));
            
            
            NavigationService.Navigate(new ClientPage(_clientService.AddClient(_client)));
        //}
        //catch
        //{
          //  MessageBox.Show("Ошибка");
        //}
        
    }
}