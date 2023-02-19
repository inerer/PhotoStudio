using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using PhotoStudio.DataBase.Repositories;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Services;

namespace PhotoStudio.Pages;

public partial class RegistrPage : Page
{
    private PersonalInfo _newPersonalInfo;
    private  Worker _newWorker;
    private PersonalInfoService _personalInfoService;
    private RoleService _roleService;
    private Role _role;
    private WorkerService _workerService;
    
    public RegistrPage()
    {
        InitializeComponent();
        _newPersonalInfo = new PersonalInfo();
        _newWorker = new Worker();
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
        throw new System.NotImplementedException();
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        if(NavigationService.CanGoBack)
            NavigationService.GoBack();
    }

    private void RegistrationButton_OnClick(object sender, RoutedEventArgs e)
    {
        GetAllInfoFromPage();
        _workerService.AddWorker(_newWorker);
    }

    private void RegistrPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        _roleService = new RoleService();
        RoleComboBox.ItemsSource = _roleService.GetAllRoles();
    }

    private void GetAllInfoFromPage()
    {
        _newWorker.PersonalInfo.LastName = LastNameTextBox.Text;
        _newWorker.PersonalInfo.FirstName = FirstNameTextBox.Text;
        _newWorker.PersonalInfo.MiddleName = MiddleNameTextBox.Text;
        _newWorker.PersonalInfo.Email = EmailTextBox.Text;
        _newWorker.PersonalInfo.MobilePhone = MobilePhoneTextBox.Text;
    }
    
}