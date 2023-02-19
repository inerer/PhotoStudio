using System.Collections.Generic;
using PhotoStudio.Models.DataBase;

namespace PhotoStudio.Services.Interfaces;

public interface IRoleInterface
{
    public Role GetRole(int id);
    public Role AddRole(Role role);
    public bool EditRole(Role role);
    public bool DeleteRole(int id);
    public List<Role> GetAllRoles();
}