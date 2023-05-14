using System;
using System.Collections.Generic;
using Npgsql;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Models.DataBase.SupplyRequestModels;
using PhotoStudio.Services.Interfaces;

namespace PhotoStudio.DataBase.Repositories;

public class HallRepository:RepositoryBase ,IHallInterface
{
    private readonly NpgsqlConnection _connection;

    public HallRepository()
    {
        
        _connection = GetConnection();
    }

    public Hall GetHall(int id)
    {
        _connection.Open();
        Hall hall = new();
        string query = "select * from hall where id_hall = $1";
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
                    hall.Id = Convert.ToInt32(reader["id_hall"]);
                    hall.Address = reader["address"].ToString();
                    hall.Description = reader["description"].ToString();
                    hall.Photo = reader["hall_photo"].ToString();
                }
            }
        }
        _connection.Close();
        return hall;
    }

    public Hall AddHall(Hall hall)
    {
        _connection.Open();
        string query = "insert into hall(description, address) values ($1, $2) returning id_hall";
        NpgsqlCommand command = new(query, _connection)
        {
            Parameters =
            {
                new NpgsqlParameter() { Value = hall.Description },
                new NpgsqlParameter() { Value = hall.Address }
            }
        };
        using (command)
        {
            // создаем reader
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                // проход по полученным данным
                while (reader.Read())
                {
                    hall.Id = Convert.ToInt32(reader["id_client"]);
                }
            }
        }
        try
        {
            return hall;
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

    public bool EditHall(Hall hall)
    {
        _connection.Open();
        string query = "update hall set description = ($1), address = ($2) where id_hall = ($3)";
        NpgsqlCommand command = new(query, _connection)
        {
            Parameters =
            {
                new NpgsqlParameter() { Value = hall.Description },
                new NpgsqlParameter() { Value = hall.Address },
                new NpgsqlParameter() { Value = hall.Id }
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

    public bool DeleteHall(int id)
    {
        _connection.Open();
        string query = "delete from hall where id_hall = ($1)";
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

    public List<Hall> GetAllHalls(Hall hall)
    {
        _connection.Open();
        List<Hall> halls = new();
        string query = "select * from hall";
        NpgsqlCommand command = new(query, _connection);
        using (command)
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    hall.Id = Convert.ToInt32(reader["id_hall"]);
                    hall.Address = reader["address"].ToString();
                    hall.Description = reader["description"].ToString();
                    hall.Photo = reader["hall_photo"].ToString();
                    halls.Add(hall);
                }
            }
        }
        _connection.Close();
        return halls;
    }
}