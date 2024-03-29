﻿using System;
using System.Collections.Generic;
using Npgsql;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Models.DataBase.SupplyRequestModels;
using PhotoStudio.Services.Interfaces;

namespace PhotoStudio.DataBase.Repositories;

public class RequestRepository:RepositoryBase ,IRequestInterface
{
    private readonly NpgsqlConnection _connection;

    public RequestRepository()
    {
        
        _connection = GetConnection();
    }

    public Request GetRequest(int id)
    {
        _connection.Open();
        Request request = new();
        string query =
            "select * from request join client  on request.id_client = client.id_client join personal_info pi on client.id_personal_info = pi.id_personal_info where id_request=($1)";
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
        _connection.Close();
        return request;
    }

    public Request AddRequest(Request request)
    {
        _connection.Open();
        string query = "insert into request(id_client) values ($1) returning id_request";
        NpgsqlCommand command = new(query, _connection)
        {
            Parameters =
            {
                new NpgsqlParameter() { Value = request.Client.Id },
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
                    request.Id = Convert.ToInt32(reader["id_request"]);
                }
            }
        }
        try
        {
            return request;
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

    public bool EditRequest(Request request)
    {
        _connection.Open();
        string query = "update request set id_client = ($1), request_timestamp = ($2) where id_request=($3)";
        NpgsqlCommand command = new(query, _connection)
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
            _connection.Close();  
        }
    }

    public bool DeleteRequest(int id)
    {
        _connection.Open();
        string query = "delete from request where id_request =($1)";
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

    public List<Request> Requests(Request request)
    {
        _connection.Open();
        List<Request> requests = new();
        string query =
            "select * from request join client  on request.id_client = client.id_client join personal_info pi on client.id_personal_info = pi.id_personal_info";
        NpgsqlCommand command = new(query, _connection);
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
        _connection.Close();
        return requests;
    }

    public Request GetRequestByClientId(Client client)
    {
        
        _connection.Open();
        Request request = new();
        string query =
            "select * from request join client  on request.id_client = client.id_client join personal_info pi on client.id_personal_info = pi.id_personal_info where client.id_client=($1)";
        NpgsqlCommand command = new(query, _connection)
        {
            Parameters = { new NpgsqlParameter() { Value = client.Id} }
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
        _connection.Close();
        return request;
    
    }
}