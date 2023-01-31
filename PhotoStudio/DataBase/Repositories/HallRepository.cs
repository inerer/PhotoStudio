using System;
using System.Collections.Generic;
using Npgsql;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Services.Interfaces;

namespace PhotoStudio.DataBase.Repositories;

public class HallRepository:IHallInterface
{
    private readonly string _connectionString;
    private  NpgsqlConnection connection;

    public HallRepository(string connectionString)
    {
        _connectionString = connectionString;
        connection = new NpgsqlConnection(_connectionString);
    }

    public Hall GetHall(int id)
    {
        connection.Open();
        Hall hall = new();
        string query = "select * from hall where id_hall = $1";
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
                    hall.Id = Convert.ToInt32(reader["id_hall"]);
                    hall.Address = reader["address"].ToString();
                    hall.Description = reader["description"].ToString();
                }
            }
        }
        connection.Close();
        return hall;
    }

    public Hall AddHall(Hall hall)
    {
        connection.Open();
        string query = "insert into hall(description, address) values ($1, $2)";
        NpgsqlCommand command = new(query, connection)
        {
            Parameters =
            {
                new NpgsqlParameter() { Value = hall.Description },
                new NpgsqlParameter() { Value = hall.Address }
            }
        };
        try
        {
            command.ExecuteNonQuery();
            return hall;
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

    public bool EditHall(Hall hall)
    {
        connection.Open();
        string query = "update hall set description = ($1), address = ($2) where id_hall = ($3)";
        NpgsqlCommand command = new(query, connection)
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
            connection.Close();
        }
    }

    public bool DeleteHall(int id)
    {
        connection.Open();
        string query = "delete from hall where id_hall = ($1)";
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

    public List<Hall> GetAllHalls(Hall hall)
    {
        connection.Open();
        List<Hall> halls = new();
        string query = "select * from hall";
        NpgsqlCommand command = new(query, connection);
        using (command)
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    hall.Id = Convert.ToInt32(reader["id_hall"]);
                    hall.Address = reader["address"].ToString();
                    hall.Description = reader["description"].ToString();
                    halls.Add(hall);
                }
            }
        }
        connection.Close();
        return halls;
    }
}