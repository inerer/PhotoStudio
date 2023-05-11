using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ModernWpf.Controls;
using PhotoStudio.DataBase.Repositories;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Services;
using PhotoStudio.Views;
using Page = System.Windows.Controls.Page;

namespace PhotoStudio.Pages;

public partial class RegistrPage : Page
{
    private readonly PersonalInfo _newPersonalInfo;
    private readonly Worker _newWorker;
    private readonly PersonalInfoService _personalInfoService;
    private RoleService _roleService;
    private Role _role;
    private readonly WorkerService _workerService;
    private PasswordValidate _passwordValidate;
    private GetHash _getHash;
    
    
    public RegistrPage()
    {
        _newWorker = new Worker();
        _newPersonalInfo = new PersonalInfo();
        InitializeComponent();
        _workerService = new WorkerService();
        _newPersonalInfo = new PersonalInfo();
        _personalInfoService = new PersonalInfoService();
        
        _role = new Role();
    }

    private void RoleComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _newWorker.Role = (Role)RoleComboBox.SelectedItem;
    }

    private async void AddRoleButton_OnClick(object sender, RoutedEventArgs e)
    {
        await AddRoleDialog();
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        if(NavigationService.CanGoBack)
            NavigationService.GoBack();
    }

    private void RegistrationButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            GetAllInfoFromPage();
            PasswordCheck();
            _newWorker.PersonalInfoId = _personalInfoService.AddPersonalInfo(_newPersonalInfo);
            _workerService.AddWorker(_newWorker);
        }
        catch (Exception exception)
        {
            MessageBox.Show("Ошибка регистрации!");
        }
        
        
    }

    private void RegistrPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        _roleService = new RoleService();
        RoleComboBox.ItemsSource = _roleService.GetAllRoles();
    }

    private void GetAllInfoFromPage()
    {
        _newPersonalInfo.FirstName = FirstNameTextBox.Text;
        _newPersonalInfo.LastName = LastNameTextBox.Text;
        _newPersonalInfo.MiddleName = MiddleNameTextBox.Text;
        _newPersonalInfo.MobilePhone = MobilePhoneTextBox.Text;
        _newPersonalInfo.Email = EmailTextBox.Text;
        _newWorker.Login = LoginTextBox.Text;
    }

    private void PasswordCheck()
    {
        _passwordValidate = new PasswordValidate();
        _getHash = new GetHash();
        if (_passwordValidate.PasswordResult(PasswordBox.Password))
            _newWorker.Password = _getHash.GetHash1(PasswordBox.Password);
        else
            MessageBox.Show("Пароль не подходит под стандарты");
    }

    private async Task AddRoleDialog()
    {
        ContentDialog contentDialog = new ContentDialog
        {
            Title = "Создание роль",
            Content = new AddRoleView(),
            CloseButtonText = "Закрыть"
        };
        await contentDialog.ShowAsync();
    }
    
}