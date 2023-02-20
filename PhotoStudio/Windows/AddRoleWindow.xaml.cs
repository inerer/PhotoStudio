using System.Windows;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Services;

namespace PhotoStudio.Windows;

public partial class AddRoleWindow : Window
{
    private Role _newRole;
    private RoleService _roleService;
    public AddRoleWindow()
    {
        InitializeComponent();
        _newRole = new Role();
        _roleService = new RoleService();
    }

    private void AddRoleButton_OnClick(object sender, RoutedEventArgs e)
    {
        
       GetInfo();
       _roleService.AddRole(_newRole);
       Close();
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void GetInfo()
    {
        _newRole.RoleName = RoleNameTextBox.Text;
    }
}