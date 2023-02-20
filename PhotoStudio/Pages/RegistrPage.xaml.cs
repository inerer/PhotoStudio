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
    private PersonalInfo _newPersonalInfo;
    private Worker _newWorker;
    private PersonalInfoService _personalInfoService;
    private RoleService _roleService;
    private Role _role;
    private WorkerService _workerService;
    
    
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

    private void AddRoleButton_OnClick(object sender, RoutedEventArgs e)
    {
        AddRoleWindow addRoleWindow = new();
        addRoleWindow.ShowDialog();
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        if(NavigationService.CanGoBack)
            NavigationService.GoBack();
    }

    private void RegistrationButton_OnClick(object sender, RoutedEventArgs e)
    {
        GetAllInfoFromPage();
        _newWorker.PersonalInfoId = _personalInfoService.AddPersonalInfo(_newPersonalInfo);
        _workerService.AddWorker(_newWorker);
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
        _newWorker.Password = PasswordBox.Password;
    }
    
}