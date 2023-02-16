using System.Windows;
using System.Windows.Controls;
using PhotoStudio.DataBase.Repositories;
using PhotoStudio.Models.DataBase;

namespace PhotoStudio.Pages;

public partial class RegistrPage : Page
{
    private PersonalInfo _newPersonalInfo;
    
    public RegistrPage()
    {
        InitializeComponent();
    }

    private void RoleComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        
    }

    private void AddRoleButton_OnClick(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void RegistrationButton_OnClick(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }
}