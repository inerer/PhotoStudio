using System;
using System.Collections.Generic;
using Npgsql;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Services;

namespace PhotoStudio.DataBase.Repositories;

public class RentRepository:IRentInterface
{
    private readonly string _connectionString;
    private NpgsqlConnection connection;


    public RentRepository(string connectionString)
    {
        _connectionString = connectionString;
        connection = new NpgsqlConnection(_connectionString);
    }

    public Rent GetRent(int id)
    {
        connection.Open();
        Rent rent = new();
        string query = "select * from rent join hall on rent.id_hall = hall.id_hall where id_rent = $1";
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
                    rent.Id = Convert.ToInt32(reader["id_rent"]);
                    rent.PriceHour = Convert.ToDecimal(reader["price_hour"]);
                    rent.Hall.Description = reader["description"].ToString();
                    rent.Hall.Address = reader["address"].ToString();
                }
            }
        }
        connection.Close();
        return rent;
    }

    public Rent AddRent(Rent rent)
    {
        connection.Open();
        string query = "insert into rent(price_hour, id_hall) values ($1, $2)";
        NpgsqlCommand command = new(query, connection)
        {
            Parameters =
            {
                new NpgsqlParameter() { Value = rent.PriceHour },
                new NpgsqlParameter() { Value = rent.Hall.Id }
            }
        };
        try
        {
            command.ExecuteNonQuery();
            return rent;
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

    public bool EditRent(Rent rent)
    {
        connection.Open();
        string query = "update rent set price = ($1), id_hall = ($2)";
        NpgsqlCommand command = new(query, connection)
        {
            Parameters =
            {
                new NpgsqlParameter() { Value = rent.PriceHour },
                new NpgsqlParameter() { Value = rent.Hall.Id }
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

    public bool DeleteRent(int id)
    {
        connection.Open();
        string query = "delete from rent where id_rent = ($1)";
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

    public List<Rent> GetAllRents(Rent rent)
    {
        connection.Open();
        List<Rent> rents = new();
        string query = "select * from rent join hall on rent.id_hall = hall.id_hall";
        NpgsqlCommand command = new(query, connection);
        using (command)
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    rent.Id = Convert.ToInt32(reader["id_rent"]);
                    rent.PriceHour = Convert.ToDecimal(reader["price_hour"]);
                    rent.Hall.Description = reader["description"].ToString();
                    rent.Hall.Address = reader["address"].ToString();
                    rents.Add(rent);
                }
            }
        }
        connection.Close();
        return rents;
            
    }
}