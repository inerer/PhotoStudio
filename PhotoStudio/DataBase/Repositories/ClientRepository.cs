using System;
using System.Collections.Generic;
using Npgsql;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Services.Interfaces;

namespace PhotoStudio.DataBase.Repositories;

public class ClientRepository:RepositoryBase ,IClientInterface
{
    private readonly NpgsqlConnection _connection;
    
    public ClientRepository()
    {
        
        _connection = GetConnection();
    }
    
    public Client GetClient(int id)
    {
        _connection.Open();
        Client client = new Client();
        string query =
            @"select * from client join personal_info on client.id_personal_info = personal_info.id_personal_info where id_client = $1";
        NpgsqlCommand cmd = new NpgsqlCommand(query, _connection)
        {
            Parameters = { new NpgsqlParameter() { Value = id } }
        };
        using (cmd)
        {
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    client.Id = reader.GetInt32(reader.GetOrdinal("id_client"));
                    client.PersonalInfo.Id = reader.GetInt32(reader.GetOrdinal("id_personal_info"));
                    client.PersonalInfo.LastName = reader.GetString(reader.GetOrdinal("last_name"));
                    client.PersonalInfo.FirstName = reader.GetString(reader.GetOrdinal("first_name"));
                    client.PersonalInfo.MiddleName = reader.GetString(reader.GetOrdinal("middle_name"));
                    client.PersonalInfo.Email = reader.GetString(reader.GetOrdinal("email"));
                    client.PersonalInfo.MobilePhone = reader.GetString(reader.GetOrdinal("mobile_phone"));
                }
            }
        }
        _connection.Close();
        return client;
    }

    public Client AddClient(Client client)
    {
        _connection.Open();
        string query = @"insert into client(id_personal_info) values ($1)";
        NpgsqlCommand command = new NpgsqlCommand(query, _connection)
        {
            Parameters =
            {
                new NpgsqlParameter() { Value = client.PersonalInfo.Id }
            }
        };
        try
        {
            command.ExecuteNonQuery();
            return client;
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

    public bool EditClient(Client client)
    {
        _connection.Open();
        string query = @"update client set id_personal_info = (@1) where id_client=($2)";
        NpgsqlCommand cmd = new(query, _connection)
        {
            Parameters =
            {
                new NpgsqlParameter() { Value = client.PersonalInfo },
                new NpgsqlParameter() { Value = client.Id }
            }
        };
        try
        {
            cmd.ExecuteNonQuery();
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

    public bool DeleteClient(int id)
    {
        Client client = new();
        _connection.Open();
        string query = @"delete from client where id_client=($1)";
        NpgsqlCommand command = new(query, _connection)
        {
            Parameters =
            {
                new NpgsqlParameter() { Value = client.Id }
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
    
    public List<Client> GetAllClients(Client client)
    {
        _connection.Open();
        List<Client> clients = new();
        string query =
            @"select * from client join personal_info on client.id_personal_info = personal_info.id_personal_info ";
        NpgsqlCommand cmd = new NpgsqlCommand(query, _connection);
        using (cmd)
        {
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    client.Id = reader.GetInt32(reader.GetOrdinal("id_client"));
                    client.PersonalInfo.Id = reader.GetInt32(reader.GetOrdinal("id_personal_info"));
                    client.PersonalInfo.LastName = reader.GetString(reader.GetOrdinal("last_name"));
                    client.PersonalInfo.FirstName = reader.GetString(reader.GetOrdinal("first_name"));
                    client.PersonalInfo.MiddleName = reader.GetString(reader.GetOrdinal("middle_name"));
                    client.PersonalInfo.Email = reader.GetString(reader.GetOrdinal("email"));
                    client.PersonalInfo.MobilePhone = reader.GetString(reader.GetOrdinal("mobile_phone"));
                    clients.Add(client);
                }
            }
        }
        _connection.Close();
        return clients;
    }
}