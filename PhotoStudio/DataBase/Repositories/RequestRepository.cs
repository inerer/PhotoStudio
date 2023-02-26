using System;
using System.Collections.Generic;
using Npgsql;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Services.Interfaces;

namespace PhotoStudio.DataBase.Repositories;

public class RequestRepository:IRequestInterface
{
    private readonly string _connectionString;
    private NpgsqlConnection connection;

    public RequestRepository(string connectionString)
    {
        _connectionString = connectionString;
        connection = new NpgsqlConnection(_connectionString);
    }

    public Request GetRequest(int id)
    {
        connection.Open();
        Request request = new();
        string query =
            "select * from request join client  on request.id_client = client.id_client join personal_info pi on client.id_personal_info = pi.id_personal_info where id_request=($1)";
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
                    request.Id = Convert.ToInt32(reader["id_request"]);
                    request.RequestTimestamp = Convert.ToDateTime(reader["request_timestamp"]);
                    request.Client.Id = Convert.ToInt32(reader["id_client"]);
                    request.Client.PersonalInfo.LastName = reader["last_name"].ToString();
                    request.Client.PersonalInfo.FirstName = reader["first_name"].ToString();
                    request.Client.PersonalInfo.MiddleName = reader["middle_name"].ToString();
                    request.Client.PersonalInfo.MobilePhone = reader["mobile_phone"].ToString();
                    request.Client.PersonalInfo.Email = reader["email"].ToString();
                }
            }
        }
        connection.Close();
        return request;
    }

    public Request AddRequest(Request request)
    {
        connection.Open();
        string query = "insert into request(id_client, request_timestamp) values ($1, $2)";
        NpgsqlCommand command = new(query, connection)
        {
            Parameters =
            {
                new NpgsqlParameter() { Value = request.Client.Id },
                new NpgsqlParameter() { Value = request.RequestTimestamp }
            }
        };
        try
        {
            command.ExecuteNonQuery();
            return request;
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

    public bool EditRequest(Request request)
    {
        connection.Open();
        string query = "update request set id_client = ($1), request_timestamp = ($2) where id_request=($3)";
        NpgsqlCommand command = new(query, connection)
        {
            Parameters =
            {
                new NpgsqlParameter() { Value = request.Client.Id },
                new NpgsqlParameter() { Value = request.RequestTimestamp },
                new NpgsqlParameter() { Value = request.Id }
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

    public bool DeleteRequest(int id)
    {
        connection.Open();
        string query = "delete from request where id_request =($1)";
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

    public List<Request> Requests(Request request)
    {
        connection.Open();
        List<Request> requests = new();
        string query =
            "select * from request join client  on request.id_client = client.id_client join personal_info pi on client.id_personal_info = pi.id_personal_info";
        NpgsqlCommand command = new(query, connection);
        using (command)
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    request = new Request
                    {
                        Id = Convert.ToInt32(reader["id_request"]),
                        RequestTimestamp = Convert.ToDateTime(reader["request_timestamp"]),
                        Client =
                        {
                            Id = Convert.ToInt32(reader["id_client"]),
                            PersonalInfo =
                            {
                                LastName = reader["last_name"].ToString(),
                                FirstName = reader["first_name"].ToString(),
                                MiddleName = reader["middle_name"].ToString(),
                                MobilePhone = reader["mobile_phone"].ToString(),
                                Email = reader["email"].ToString()
                            }
                        }
                    };
                    requests.Add(request);
                }
            }
        }
        connection.Close();
        return requests;
    }
}