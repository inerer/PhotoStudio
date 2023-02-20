using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using PhotoStudio.DataBase.Repositories;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Services.Interfaces;

namespace PhotoStudio.Services;

public class RoleService:IRoleInterface
{
    private readonly string _connectionString;
    private readonly RoleRepository _roleRepository;

    public RoleService()
    {
        var configBuilder = new ConfigurationBuilder().AddJsonFile( "C:/Users/arshi/RiderProjects/PhotoStudio/PhotoStudio/appsettings.json").Build();
        var configSection = configBuilder.GetSection("ConnectionStrings");
        _connectionString = configSection["PhotoStudioDB"] ?? null;
        _roleRepository = new RoleRepository(_connectionString);
    }

    public Role GetRole(int id)
    {
        throw new System.NotImplementedException();
    }

    public Role AddRole(Role role)
    {
        return _roleRepository.AddRole(role);
    }

    public bool EditRole(Role role)
    {
        throw new System.NotImplementedException();
    }

    public bool DeleteRole(int id)
    {
        throw new System.NotImplementedException();
    }

    public List<Role> GetAllRoles()
    {
        return _roleRepository.GetAllRoles();
    }
}