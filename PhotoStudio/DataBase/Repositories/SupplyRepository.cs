using System;
using System.Collections.Generic;
using Npgsql;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Services.Interfaces;

namespace PhotoStudio.DataBase.Repositories;

public class SupplyRepository:ISupplyInterface
{
    private readonly string _connectionString;
    private NpgsqlConnection connection;

    public SupplyRepository(string connectionString)
    {
        _connectionString = connectionString;
        connection = new NpgsqlConnection(_connectionString);
    }

    public Supply GetSupply(int id)
    {
        connection.Open();
        Supply supply = new();
        string query =
            "select * from supply join type_supply ts on supply.id_type_supply = ts.id_type_supply join rent r on supply.id_rent = r.id_rent where id_supply=($1)";
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
        connection.Close();
        return supply;
    }

    public Supply AddSupply(Supply supply)
    {
        throw new System.NotImplementedException();
    }

    public bool EditSupply(Supply supply)
    {
        throw new System.NotImplementedException();
    }

    public bool DeleteSupply(int id)
    {
        throw new System.NotImplementedException();
    }

    public List<Supply> GetAllSupplies(Supply supply)
    {
        throw new System.NotImplementedException();
    }
}