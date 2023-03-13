using System;
using System.Collections.Generic;
using System.Windows.Documents;
using Npgsql;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Services.Interfaces;

namespace PhotoStudio.DataBase.Repositories;

public class RoleRepository:RepositoryBase ,IRoleInterface
{
    private readonly NpgsqlConnection _connection;

    public RoleRepository()
    {
        
        _connection = GetConnection();
    }

    public Role GetRole(int id)
    {
        _connection.Open();
        Role role = new();
        string query = "select * from role where id_role = $1";
        NpgsqlCommand command = new(query, _connection)
        {
            Parameters = { new NpgsqlParameter() { Value = id } }
        };
        using (command)
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    role.Id = Convert.ToInt32(reader["id_role"]);
                    role.RoleName = reader["role_name"].ToString();
                }
            }
        }
        _connection.Close();
        return role;
    }

    public Role AddRole(Role role)
    {
        _connection.Open();
        string query = "insert into role(role_name) values ($1)";
        NpgsqlCommand command = new(query, _connection)
        {
            Parameters = { new NpgsqlParameter() { Value = role.RoleName } }
        };
        try
        {
            command.ExecuteNonQuery();
            return role;
        }
        catch (Exception e)
        {
            return null;
        }
        finally
        {
            _connection.Close();  
        }
    }

    public bool EditRole(Role role)
    {
        _connection.Open();
        string query = "update role set role_name=($1) where id_role =($2)";
        NpgsqlCommand command = new(query, _connection)
        {
            Parameters =
            {
                new NpgsqlParameter() { Value = role.RoleName }, 
                new NpgsqlParameter() {Value = role.Id}
            }
        };
        try
        {
            command.ExecuteNonQuery();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
        finally
        {
            _connection.Close();  
        }
    }

    public bool DeleteRole(int id)
    {
        _connection.Open();
        string query = "delete from role where id_role = ($1)";
        NpgsqlCommand command = new(query, _connection)
        {
            Parameters = { new NpgsqlParameter() { Value = id } }
        };
        try
        {
            command.ExecuteNonQuery();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
        finally
        {
            _connection.Close();
        }
    }

    public List<Role> GetAllRoles()
    {
        _connection.Open();
        List<Role> roles = new();
        string query = "select * from role";
        NpgsqlCommand command = new(query, _connection);
        using (command)
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Role role = new Role
                    {
                        Id = Convert.ToInt32(reader["id_role"]),
                        RoleName = reader["role_name"].ToString()
                    };
                    roles.Add(role);
                }
            }
        }
        _connection.Close();
        return roles;
    }
}