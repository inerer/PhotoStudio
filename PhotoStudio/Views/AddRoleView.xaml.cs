using System.Windows;
using System.Windows.Controls;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Services;

namespace PhotoStudio.Views;

public partial class AddRoleView : UserControl
{
    private readonly Role _newRole;
    private readonly RoleService _roleService;
    public AddRoleView()
    {
        InitializeComponent();
        _newRole = new Role();
        _roleService = new RoleService();
        this.DataContext = _newRole;
    }

    private void AddRoleButton_OnClick(object sender, RoutedEventArgs e)
    {
        _roleService.AddRole(_newRole);
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }
}