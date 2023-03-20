using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PhotoStudio.DataBase.Repositories;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Services;

namespace PhotoStudio.Pages;

public partial class AuthPage : Page
{
    private string? _login;
    private string? _password;
    private GetHash _getHash;
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

    private void Login()
    {
        GetData();
        if (_workerService.Auth(_login, _password))
        {
            Worker worker = _workerService.GetWorkerByLogin(_login);
                NavigationService.Navigate(new MainPage(worker));
        }
        else
        {
            MessageBox.Show("Не удалось зайти");
        }
        
    }

    private void GetData()
    {
        _getHash = new GetHash();
        _login = LoginTextBox.Text;

        _password = PasswordBox.Password;

        _password = _getHash.GetHash1(_password);
    }

    private void RegistrationTextBlock_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        NavigationService.Navigate(new RegistrPage());
    }

    private void GuestButton_OnClick(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new ClientPage());
    }
}