using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using PhotoStudio.DataBase.Repositories;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Services;
using PhotoStudio.Windows;

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
    private PhoneNumberValidate _phoneNumberValidate;
    
    
    public RegistrPage()
    {
        InitializeComponent();
        _workerService = new WorkerService();
        _newPersonalInfo = new PersonalInfo();
        _personalInfoService = new PersonalInfoService();
        _role = new Role();
        _newWorker = new Worker();
        _newPersonalInfo = new PersonalInfo();
    }
    
    private void RoleComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _newWorker.Role = (Role)RoleComboBox.SelectedItem;
    }
    
    private void AddRoleButton_OnClick(object sender, RoutedEventArgs e)
    {
        AddRoleWindow addRoleWindow = new();
        addRoleWindow.ShowDialog();
        RoleComboBox.ItemsSource = _roleService.GetAllRoles();
    }
    
    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        if(NavigationService.CanGoBack)
            NavigationService.GoBack();
    }

    private void RegistrationButton_OnClick(object sender, RoutedEventArgs e)
    {
        GetAllInfoFromPage();
        if (PasswordCheck() &&  PhoneNumberCheck() )
        {
            _newWorker.PersonalInfoId = _personalInfoService.AddPersonalInfo(_newPersonalInfo);
            _workerService.AddWorker(_newWorker);
        }
        else
        {
            MessageBox.Show("Регистрация прошла не удачно");
        }
    }
    
    private void RegistrPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        _roleService = new RoleService();
        RoleComboBox.ItemsSource = _roleService.GetAllRoles();
    }
    
    private void GetAllInfoFromPage()
    {
        try
        {
            _newPersonalInfo.FirstName = FirstNameTextBox.Text;
            _newPersonalInfo.LastName = LastNameTextBox.Text;
            _newPersonalInfo.MiddleName = MiddleNameTextBox.Text;
            _newPersonalInfo.Email = EmailTextBox.Text;
            _newWorker.Login = LoginTextBox.Text;
        }
        catch 
        {
            MessageBox.Show("Ошибка!");
        }
        
    }
    
    private bool PhoneNumberCheck()
    {
        _phoneNumberValidate = new PhoneNumberValidate();
        if (_phoneNumberValidate.CheckFirstSymbol(MobilePhoneTextBox.Text))
        {
            _newPersonalInfo.MobilePhone = MobilePhoneTextBox.Text;
            return true;
        }
        else
        {
            return false;
            MessageBox.Show("Неверный номер");
        }
    }

    private bool PasswordCheck()
    {
        _passwordValidate = new PasswordValidate();
        _getHash = new GetHash();
        if (_passwordValidate.PasswordResult(PasswordBox.Password))
        {
            _newWorker.Password = _getHash.GetHash1(PasswordBox.Password);
            return true;
        }
        else
        {
            MessageBox.Show("Пароль не подходит под стандарты");
            return false;
        }
    }
}