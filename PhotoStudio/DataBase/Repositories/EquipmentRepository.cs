using System;
using System.Collections.Generic;
using Npgsql;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Services.Interfaces;

namespace PhotoStudio.DataBase.Repositories;

public class EquipmentRepository:IEquipmentInterface
{
    private readonly string _connectionString;
    private NpgsqlConnection connection;

    public EquipmentRepository(string connectionString)
    {
        _connectionString = connectionString;
        connection = new NpgsqlConnection(_connectionString);
    }

    public Equipment GetEquipment(int id)
    {
        connection.Open();
        Equipment equipment = new();
        string query = "select * from equipment where id_equipment=$1";
        NpgsqlCommand command = new(query, connection)
        {
            Parameters = { new NpgsqlParameter() { Value = id } }
        };
        using (command)
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    equipment.Id = Convert.ToInt32(reader["id_equipment"]);
                    equipment.Name = reader["equipment_name"].ToString();
                }
            }
        }
        connection.Close();
        return equipment;
    }

    public Equipment AddEquipment(Equipment equipment)
    {
        connection.Open();
        string query = "insert into equipment(equipment_name) values ($1)";
        NpgsqlCommand command = new(query, connection)
        {
            Parameters = { new NpgsqlParameter() { Value = equipment.Name } }
        };
        try
        {
            command.ExecuteNonQuery();
            return equipment;
        }
        catch (Exception e)
        {
            return null;
        }
        finally
        {
            connection.Close();  
        } 
    }

    public bool EditEquipment(Equipment equipment)
    {
        connection.Open();
        string query = "update equipment set equipment_name = ($1) where id_equipment = ($2)";
        NpgsqlCommand command = new(query, connection)
        {
            Parameters =
            {
                new NpgsqlParameter() { Value = equipment.Name },
                new NpgsqlParameter() { Value = equipment.Id }
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
            connection.Close();
        }
    }

    public bool DeleteEquipment(int id)
    {
        connection.Open();
        string query = "delete from equipment where id_equipment = ($1)";
        NpgsqlCommand command = new(query, connection)
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
            connection.Close();
        }
    }

    public List<Equipment> GetAllEquipments(Equipment equipment)
    {
        connection.Open();
        List<Equipment> equipments = new();
        string query = "select * from equipment";
        NpgsqlCommand command = new(query, connection);
        using (command)
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    equipment.Id = Convert.ToInt32(reader["id_equipment"]);
                    equipment.Name = reader["equipment_name"].ToString();
                    equipments.Add(equipment);
                }
            }
        }
        connection.Close();
        return equipments; 
    }
}