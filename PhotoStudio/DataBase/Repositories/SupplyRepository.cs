using System;
using System.Collections.Generic;
using Npgsql;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Models.DataBase.SupplyRequestModels;
using PhotoStudio.Services.Interfaces;

namespace PhotoStudio.DataBase.Repositories;

public class SupplyRepository:RepositoryBase ,ISupplyInterface
{
    private readonly NpgsqlConnection _connection;

    public SupplyRepository()
    {
        
        _connection = GetConnection();
    }

    public Supply GetSupply(int id)
    {
        _connection.Open();
        Supply supply = new();
        string query =
            "select * from supply join type_supply ts on supply.id_type_supply = ts.id_type_supply join rent r on supply.id_rent = r.id_rent where id_supply=($1)";
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
                    supply.Id = Convert.ToInt32(reader["id_supply"]);
                    supply.Name = reader["supply_name"].ToString();
                    supply.Price = Convert.ToDecimal(reader["price_supply"]);
                    supply.Description = reader["supply_description"].ToString();
                    supply.SupplyTimestamp = Convert.ToDateTime(reader["timestamp_supply"]);
                    supply.TypeSupply.Name = reader["type_supply_name"].ToString();
                    supply.Rent.PriceHour = Convert.ToDecimal(reader["price_hour"]);
                    supply.Rent.Hall.Description = reader["description"].ToString();
                    supply.Rent.Hall.Address = reader["address"].ToString();
                }
            }
        }
        _connection.Close();
        return supply;
    }

    public Supply AddSupply(Supply supply)
    {
        _connection.Open();
        string query =
            "insert into supply(id_type_supply, id_rent, supply_name, price_supply, supply_description, timestamp_supply) values ($1, $2, $3, $4,$5,$6)";
        NpgsqlCommand command = new(query, _connection)
        {
            Parameters =
            {
                new NpgsqlParameter() { Value = supply.TypeSupply.Id },
                new NpgsqlParameter() { Value = supply.Rent.Id },
                new NpgsqlParameter() { Value = supply.Name },
                new NpgsqlParameter() { Value = supply.Price },
                new NpgsqlParameter() { Value = supply.Description },
                new NpgsqlParameter() { Value = supply.SupplyTimestamp }
            }
        };
        try
        {
            command.ExecuteNonQuery();
            return supply;
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

    public bool EditSupply(Supply supply)
    {
        _connection.Open();
        string query =
            "update supply set id_type_supply = ($1), id_rent = ($2), supply_name = ($3), price_supply=($4), supply_description=($5), timestamp_supply=($6) where id_supply=($7)";
        NpgsqlCommand command = new(query, _connection)
        {
            Parameters =
            {
                new NpgsqlParameter() { Value = supply.TypeSupply.Id },
                new NpgsqlParameter() { Value = supply.Rent.Id },
                new NpgsqlParameter() { Value = supply.Name },
                new NpgsqlParameter() { Value = supply.Price },
                new NpgsqlParameter() { Value = supply.Description },
                new NpgsqlParameter() { Value = supply.SupplyTimestamp },
                new NpgsqlParameter() { Value = supply.Id }
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

    public bool DeleteSupply(int id)
    {
        _connection.Open();
        string query = "delete from supply where id_supply = ($1)";
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

    public List<Supply> GetAllSupplies(Supply supply)
    {
        _connection.Open();
        List<Supply> supplies = new();
        string query =
            "select * from supply join type_supply ts on supply.id_type_supply = ts.id_type_supply join rent r on supply.id_rent = r.id_rent join hall h on r.id_hall = h.id_hall";
        NpgsqlCommand command = new(query, _connection);
        using (command)
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    supply.Id = Convert.ToInt32(reader["id_supply"]);
                    supply.Name = reader["supply_name"].ToString();
                    supply.Price = reader.GetDecimal(reader.GetOrdinal("price_supply"));
                    supply.Description = reader["supply_description"].ToString();
                    supply.SupplyTimestamp = Convert.ToDateTime(reader["timestamp_supply"]);
                    supply.TypeSupply.Name = reader["type_supply_name"].ToString();
                    supply.Rent.PriceHour = Convert.ToDecimal(reader["price_hour"]);
                    supply.Rent.Hall.Description = reader["description"].ToString();
                    supply.Rent.Hall.Address = reader["address"].ToString();
                    // supply.Rent.Hall.Photo = reader.GetString(reader.GetOrdinal("hall_photo"));
                    supplies.Add(supply);
                }
            }
        }
        _connection.Close();
        return supplies;
    }
}