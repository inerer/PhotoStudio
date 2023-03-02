using System;
using System.Collections.Generic;
using Npgsql;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Services.Interfaces;

namespace PhotoStudio.DataBase.Repositories;

public class SupplyRequestRepository:ISupplyRequestInterface
{
    private readonly string _connectionString;
    private NpgsqlConnection connection;

    public SupplyRequestRepository(string connectionString)
    {
        _connectionString = connectionString;
        connection = new NpgsqlConnection(_connectionString);
    }

    public SupplyRequest GetSupplyRequest(int id)
    {
        connection.Open();
        SupplyRequest supplyRequest = new();
        string query =
            "select * from supply_request join supply s on supply_request.id_supply = s.id_supply join request r on supply_request.id_request = r.id_request join rent r2 on s.id_rent = r2.id_rent join client c on r.id_client = c.id_client where id_supply_request=($1)";
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
                    supplyRequest.Id = Convert.ToInt32(reader["id_supply_request"]);
                    supplyRequest.Supply.Name = reader["supply_name"].ToString();
                    supplyRequest.Supply.Price = Convert.ToDecimal(reader["price_supply"]);
                    supplyRequest.Supply.Description = reader["supply_description"].ToString();
                    supplyRequest.Supply.SupplyTimestamp = Convert.ToDateTime(reader["timestamp_supply"]);
                    supplyRequest.Supply.TypeSupply.Name = reader["type_supply_name"].ToString();
                    supplyRequest.Supply.Rent.PriceHour = Convert.ToDecimal(reader["price_hour"]);
                    supplyRequest.Supply.Rent.Hall.Description = reader["description"].ToString();
                    supplyRequest.Supply.Rent.Hall.Address = reader["address"].ToString();
                    supplyRequest.Request.RequestTimestamp = Convert.ToDateTime(reader["request_timestamp"]);
                    supplyRequest.Request.Client.Id = Convert.ToInt32(reader["id_client"]);
                    supplyRequest.Request.Client.PersonalInfo.LastName = reader["last_name"].ToString();
                    supplyRequest.Request.Client.PersonalInfo.FirstName = reader["first_name"].ToString();
                    supplyRequest.Request.Client.PersonalInfo.MiddleName = reader["middle_name"].ToString();
                    supplyRequest.Request.Client.PersonalInfo.MobilePhone = reader["mobile_phone"].ToString();
                    supplyRequest.Request.Client.PersonalInfo.Email = reader["email"].ToString();
                }
            }
        }
        connection.Close();
        return supplyRequest;
    }

    public SupplyRequest AddSupplyRequest(SupplyRequest supplyRequest)
    {
        connection.Open();
        string query = "insert into supply_request(id_supply, id_request) values ($1, $2)";
        NpgsqlCommand command = new(query, connection)
        {
            Parameters =
            {
                new NpgsqlParameter() { Value = supplyRequest.Supply.Id },
                new NpgsqlParameter() { Value = supplyRequest.Request.Id }
            }
        };
        try
        {
            command.ExecuteNonQuery();
            return supplyRequest;
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

    public bool EditSupplyRequest(SupplyRequest supplyRequest)
    {
        connection.Open();
        string query = "update supply_request set id_supply = ($1), id_request = ($2) where  id_supply_request = ($3)";
        NpgsqlCommand command = new(query, connection)
        {
            Parameters =
            {
                new NpgsqlParameter() { Value = supplyRequest.Supply.Id },
                new NpgsqlParameter() { Value = supplyRequest.Request.Id },
                new NpgsqlParameter() { Value = supplyRequest.Id }
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

    public bool DeleteSupplyRequest(int id)
    {
        connection.Open();
        string query = "delete from supply_request where id_supply_request = ($1)";
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

    public List<SupplyRequest> GetAllSupplyRequests(SupplyRequest supplyRequest)
    {
        connection.Open();
        List<SupplyRequest> supplyRequests = new();
        string query =
            "select * from supply_request join supply s on supply_request.id_supply = s.id_supply join request r on supply_request.id_request = r.id_request";
        NpgsqlCommand command = new(query, connection);
        using (command)
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    supplyRequest.Id = Convert.ToInt32(reader["id_supply_request"]);
                    supplyRequest.Supply.Name = reader["supply_name"].ToString();
                    supplyRequest.Supply.Price = Convert.ToDecimal(reader["price_supply"]);
                    supplyRequest.Supply.Description = reader["supply_description"].ToString();
                    supplyRequest.Supply.SupplyTimestamp = Convert.ToDateTime(reader["timestamp_supply"]);
                    supplyRequest.Supply.TypeSupply.Name = reader["type_supply_name"].ToString();
                    supplyRequest.Supply.Rent.PriceHour = Convert.ToDecimal(reader["price_hour"]);
                    supplyRequest.Supply.Rent.Hall.Description = reader["description"].ToString();
                    supplyRequest.Supply.Rent.Hall.Address = reader["address"].ToString();
                    supplyRequest.Request.RequestTimestamp = Convert.ToDateTime(reader["request_timestamp"]);
                    supplyRequest.Request.Client.Id = Convert.ToInt32(reader["id_client"]);
                    supplyRequest.Request.Client.PersonalInfo.LastName = reader["last_name"].ToString();
                    supplyRequest.Request.Client.PersonalInfo.FirstName = reader["first_name"].ToString();
                    supplyRequest.Request.Client.PersonalInfo.MiddleName = reader["middle_name"].ToString();
                    supplyRequest.Request.Client.PersonalInfo.MobilePhone = reader["mobile_phone"].ToString();
                    supplyRequest.Request.Client.PersonalInfo.Email = reader["email"].ToString();
                    supplyRequests.Add(supplyRequest);
                }
            }
        }
        connection.Close();
        return supplyRequests;
    }

    public List<SupplyRequest> GelSupplyRequestByRequestId(Request request)
    {
        connection.Open();
        SupplyRequest supplyRequest = new();
        List<SupplyRequest> supplyRequests = new();
        string query =
            "select * from supply_request join supply s on supply_request.id_supply = s.id_supply join request r on supply_request.id_request = r.id_request join rent r2 on s.id_rent = r2.id_rent join client c on r.id_client = c.id_client join type_supply ts on s.id_type_supply = ts.id_type_supply join hall h on r2.id_hall = h.id_hall where r.id_request=($1)";
        NpgsqlCommand command = new(query, connection)
        {
            Parameters = { new NpgsqlParameter() { Value = request.Id } }
        };
        using (command)
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    supplyRequest.Id = Convert.ToInt32(reader["id_supply_request"]);
                    supplyRequest.Supply.Name = reader["supply_name"].ToString();
                    supplyRequest.Supply.Price = Convert.ToDecimal(reader["price_supply"]);
                    supplyRequest.Supply.Description = reader["supply_description"].ToString();
                    supplyRequest.Supply.SupplyTimestamp = Convert.ToDateTime(reader["timestamp_supply"]);
                    supplyRequest.Supply.TypeSupply.Name = reader["type_supply_name"].ToString();
                    supplyRequest.Supply.Rent.PriceHour = Convert.ToDecimal(reader["price_hour"]);
                    supplyRequest.Supply.Rent.Hall.Description = reader["description"].ToString();
                    supplyRequest.Supply.Rent.Hall.Address = reader["address"].ToString();
                   // supplyRequest.Request.RequestTimestamp = Convert.ToDateTime(reader["request_timestamp"]);
                   // supplyRequest.Request.Client.Id = Convert.ToInt32(reader["id_client"]);
                   // supplyRequest.Request.Client.PersonalInfo.LastName = reader["last_name"].ToString();
                   // supplyRequest.Request.Client.PersonalInfo.FirstName = reader["first_name"].ToString();
                    //supplyRequest.Request.Client.PersonalInfo.MiddleName = reader["middle_name"].ToString();
                    //supplyRequest.Request.Client.PersonalInfo.MobilePhone = reader["mobile_phone"].ToString();
                    //supplyRequest.Request.Client.PersonalInfo.Email = reader["email"].ToString();
                }
            }
        }
        connection.Close();
        supplyRequest.Request = request;
        supplyRequests.Add(supplyRequest);
        return supplyRequests;
    }
}