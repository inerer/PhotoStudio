using System;
using System.Collections.Generic;
using Npgsql;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Models.DataBase.SupplyRequestModels;
using PhotoStudio.Services.Interfaces;

namespace PhotoStudio.DataBase.Repositories;

public class TypeSupplyRepository:RepositoryBase ,ITypeSupplyInterface
{
    private readonly NpgsqlConnection _connection;

    public TypeSupplyRepository()
    {
        _connection = GetConnection();
    }

    public TypeSupply GetTypeSupply(int id)
    {
        _connection.Open();
        TypeSupply typeSupply = new();
        string query = "select * from type_supply where id_type_supply = ($1)";
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
                    typeSupply.Id = Convert.ToInt32(reader["id_type_supply"]);
                    typeSupply.Name = reader["type_supply_name"].ToString();
                    
                }
            }
        }
        _connection.Close();
        return typeSupply;
    }

    public TypeSupply AddTypeSupply(TypeSupply typeSupply)
    {
       _connection.Open();
       string query = "insert into type_supply(type_supply_name) values ($1)";
       NpgsqlCommand command = new(query, _connection)
       {
           Parameters = { new NpgsqlParameter() { Value = typeSupply.Name } }
       };
       try
       {
           command.ExecuteNonQuery();
           return typeSupply;
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

    public bool EditTypeSupply(TypeSupply typeSupply)
    {
        _connection.Open();
        string query = "update type_supply set type_supply_name = ($1) where id_type_supply=($2)";
        NpgsqlCommand command = new(query, _connection)
        {
            Parameters =
            {
                new NpgsqlParameter() { Value = typeSupply.Name, },
                new NpgsqlParameter() { Value = typeSupply.Id }
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

    public bool DeleteTypeSupply(int id)
    {
        _connection.Open();
        string query = "delete from type_supply where id_type_supply = ($1)";
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

    public List<TypeSupply> GetAllTypeSupplies(TypeSupply typeSupply)
    {
        _connection.Open();
        List<TypeSupply> typeSupplies = new();
        string query = "select * from type_supply";
        NpgsqlCommand command = new(query, _connection);
        using (command)
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    typeSupply.Id = Convert.ToInt32(reader["id_type_supply"]);
                    typeSupply.Name = reader["type_supply_name"].ToString();
                    typeSupplies.Add(typeSupply);
                    
                }
            }
        }
        _connection.Close();
        return typeSupplies;
    }
}