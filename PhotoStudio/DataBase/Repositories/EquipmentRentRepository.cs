using System;
using System.Collections.Generic;
using Npgsql;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Services.Interfaces;

namespace PhotoStudio.DataBase.Repositories;

public class EquipmentRentRepository:IEquipmentRentInterface
{
    private readonly string _connectionString;
    private  NpgsqlConnection connection;

    public EquipmentRentRepository(string connectionString)
    {
        _connectionString = connectionString;
        connection = new NpgsqlConnection(_connectionString);
    }

    public EquipmentRent GetEquipmentRent(int id)
    {
        connection.Open();
        EquipmentRent equipmentRent = new();
        string query =
            "select * from equipment_rent join equipment on equipment_rent.id_equipment = equipment.id_equipment join rent on equipment_rent.id_rent = rent.id_rent join hall on rent.id_hall = hall.id_hall where id_request_rent = ($1)";
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
                    equipmentRent.Id = Convert.ToInt32(reader["id_request_rent"]);
                    equipmentRent.Equipment.Name = reader["equpment_name"].ToString();
                    equipmentRent.Rent.PriceHour = Convert.ToDecimal(reader["price_hour"]);
                    equipmentRent.Rent.Hall.Description = reader["description"].ToString();
                    equipmentRent.Rent.Hall.Address = reader["address"].ToString();
                }
            }
        }
        connection.Close();
        return equipmentRent;
    }

    public EquipmentRent AddEquipmentRent(EquipmentRent equipmentRent)
    {
        connection.Open();
        string query = "insert into equipment_rent(id_equipment, id_rent) values ($1, $2)";
        NpgsqlCommand command = new(query, connection)
        {
            Parameters =
            {
                new NpgsqlParameter() { Value = equipmentRent.Equipment.Id },
                new NpgsqlParameter() { Value = equipmentRent.Rent.Id }
            }
        };
        try
        {
            command.ExecuteNonQuery();
            return equipmentRent;
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

    public bool EditEquipmentRent(EquipmentRent equipmentRent)
    {
        connection.Open();
        string query = "update equipment_rent set id_equipment = ($1), id_rent=($2) where id_equipment = ($3)";
        NpgsqlCommand command = new(query, connection)
        {
            Parameters =
            {
                new NpgsqlParameter() { Value = equipmentRent.Equipment.Id },
                new NpgsqlParameter() { Value = equipmentRent.Rent.Id },
                new NpgsqlParameter() {Value = equipmentRent.Id}
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

    public bool DeleteEquipmentRent(EquipmentRent equipmentRent)
    {
        connection.Open();
        string query = "delete from equipment_rent where id_equipment = ($1)";
        NpgsqlCommand command = new(query, connection)
        {
            Parameters = { new NpgsqlParameter() { Value = equipmentRent.Id } }
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

    public List<EquipmentRent> GelAllEquipmentRents(EquipmentRent equipmentRent)
    {
        connection.Open();
        List<EquipmentRent> equipmentRents = new();
        string query =
            "select * from equipment_rent join equipment on equipment_rent.id_equipment = equipment.id_equipment join rent on equipment_rent.id_rent = rent.id_rent join hall on rent.id_hall = hall.id_hall";
        NpgsqlCommand command = new(query, connection);
        using (command)
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    equipmentRent.Id = Convert.ToInt32(reader["id_request_rent"]);
                    equipmentRent.Equipment.Name = reader["equpment_name"].ToString();
                    equipmentRent.Rent.PriceHour = Convert.ToDecimal(reader["price_hour"]);
                    equipmentRent.Rent.Hall.Description = reader["description"].ToString();
                    equipmentRent.Rent.Hall.Address = reader["address"].ToString();
                    equipmentRents.Add(equipmentRent);
                }
            }
        }
        connection.Close();
        return equipmentRents;
    }
}