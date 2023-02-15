using System.Windows;
using System.Windows.Controls;
using PhotoStudio.DataBase.Repositories;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Services;

namespace PhotoStudio.Pages;

public partial class AuthPage : Page
{
    private string? _login;
    private string? _password;
    private readonly WorkerService _workerService;
    public AuthPage()
    {
        
        InitializeComponent();
        _workerService = new WorkerService();
    }

    private void LoginButton_OnClick(object sender, RoutedEventArgs e)
    {
        Login();
    }

    private void RegisterButton_OnClick(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void Login()
    {
        GetData();
        if (_workerService.Auth(_login, _password))
        {
            Worker worker = _workerService.GetWorkerByLogin(_login);
            NavigationService.Navigate(new NextPAge());
        }
        else
        {
            MessageBox.Show("Иди в попу");
        }
        
    }

    private void GetData()
    {
        _login = LoginTextBox.Text;
        _password = PasswordBox.Password;
    }
}