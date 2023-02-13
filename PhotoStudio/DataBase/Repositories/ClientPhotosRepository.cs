using System;
using System.Collections.Generic;
using Npgsql;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Services.Interfaces;

namespace PhotoStudio.DataBase.Repositories;

public class ClientPhotosRepository:IClientPhotosInterface
{
    private readonly string _connectionString;
    private NpgsqlConnection connection;

    public ClientPhotosRepository(string connectionString)
    {
        _connectionString = connectionString;
        connection = new NpgsqlConnection(_connectionString);
    }

    public ClientPhotos GetClientPhotos(int id)
    {
        connection.Open();
        ClientPhotos clientPhotos = new();
        string query =
            "select * from client_photos join request r on r.id_request = client_photos.id_request where id_client_photos=($1)";
        NpgsqlCommand command = new(query, connection)
        {
            Parameters =
            {
                new NpgsqlParameter() { Value = id }
            }
        };
        using (command)
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    clientPhotos.Id = Convert.ToInt32(reader["id_client_photos"]);
                    clientPhotos.Photo = reader["photo"].ToString();
                    clientPhotos.Request.Id = Convert.ToInt32(reader["id_request"]);
                    clientPhotos.Request.RequestTimestamp = Convert.ToDateTime(reader["request_timestamp"]);
                    clientPhotos.Request.Client.Id = Convert.ToInt32(reader["id_client"]);
                    clientPhotos.Request.Client.PersonalInfo.LastName = reader["last_name"].ToString();
                    clientPhotos.Request.Client.PersonalInfo.FirstName = reader["first_name"].ToString();
                    clientPhotos.Request.Client.PersonalInfo.MiddleName = reader["middle_name"].ToString();
                    clientPhotos.Request.Client.PersonalInfo.MobilePhone = reader["mobile_phone"].ToString();
                    clientPhotos.Request.Client.PersonalInfo.Email = reader["email"].ToString();
                }
            }
        }
        connection.Close();
        return clientPhotos;
    }

    public List<ClientPhotos> GetPhotoForRequest(int id)
    {
        connection.Open();
        ClientPhotos clientPhotos = new();
        List<ClientPhotos> clientPhotosList = new();
        string query =
            "select * from client_photos join request r on r.id_request = client_photos.id_request where r.id_request=($1)";
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
                    clientPhotos.Id = Convert.ToInt32(reader["id_client_photos"]);
                    clientPhotos.Photo = reader["photo"].ToString();
                    clientPhotos.Request.Id = Convert.ToInt32(reader["id_request"]);
                    clientPhotos.Request.RequestTimestamp = Convert.ToDateTime(reader["request_timestamp"]);
                    clientPhotos.Request.Client.Id = Convert.ToInt32(reader["id_client"]);
                    clientPhotos.Request.Client.PersonalInfo.LastName = reader["last_name"].ToString();
                    clientPhotos.Request.Client.PersonalInfo.FirstName = reader["first_name"].ToString();
                    clientPhotos.Request.Client.PersonalInfo.MiddleName = reader["middle_name"].ToString();
                    clientPhotos.Request.Client.PersonalInfo.MobilePhone = reader["mobile_phone"].ToString();
                    clientPhotos.Request.Client.PersonalInfo.Email = reader["email"].ToString();
                    clientPhotosList.Add(clientPhotos);
                }
            }
        }
        connection.Close();
        return clientPhotosList;
    }

    public ClientPhotos AddClientPhotos(ClientPhotos clientPhotos)
    {
        connection.Open();
        string query = "insert into client_photos(photo, id_request) values ($1, $2)";
        NpgsqlCommand command = new(query, connection)
        {
            Parameters =
            {
                new NpgsqlParameter() { Value = clientPhotos.Photo },
                new NpgsqlParameter() { Value = clientPhotos.Request.Id }
            }
        };
        try
        {
            command.ExecuteNonQuery();
            return clientPhotos;
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

    public bool EditClientPhotos(ClientPhotos clientPhotos)
    {
        connection.Open();
        string query = "update client_photos set photo = ($1), id_request = ($2) where id_client_photos =($3)";
        NpgsqlCommand command = new(query, connection)
        {
            Parameters =
            {
                new NpgsqlParameter() { Value = clientPhotos.Photo },
                new NpgsqlParameter() { Value = clientPhotos.Request.Id },
                new NpgsqlParameter() { Value = clientPhotos.Id }
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

    public bool DeleteClientPhotos(int id)
    {
        connection.Open();
        string query = "delete from client_photos where id_client_photos= ($1)";
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

    public List<ClientPhotos> GetAllClientPhotosList(ClientPhotos clientPhotos)
    {
       connection.Open();
       List<ClientPhotos> clientPhotosList = new();
       string query =
           "select * from client_photos join request r on r.id_request = client_photos.id_request";
       NpgsqlCommand command = new(query, connection);
       using (command)
       {
           using (NpgsqlDataReader reader = command.ExecuteReader())
           {
               while (reader.Read())
               {
                   clientPhotos.Id = Convert.ToInt32(reader["id_client_photos"]);
                   clientPhotos.Photo = reader["photo"].ToString();
                   clientPhotos.Request.Id = Convert.ToInt32(reader["id_request"]);
                   clientPhotos.Request.RequestTimestamp = Convert.ToDateTime(reader["request_timestamp"]);
                   clientPhotos.Request.Client.Id = Convert.ToInt32(reader["id_client"]);
                   clientPhotos.Request.Client.PersonalInfo.LastName = reader["last_name"].ToString();
                   clientPhotos.Request.Client.PersonalInfo.FirstName = reader["first_name"].ToString();
                   clientPhotos.Request.Client.PersonalInfo.MiddleName = reader["middle_name"].ToString();
                   clientPhotos.Request.Client.PersonalInfo.MobilePhone = reader["mobile_phone"].ToString();
                   clientPhotos.Request.Client.PersonalInfo.Email = reader["email"].ToString();
                   clientPhotosList.Add(clientPhotos);
               }
           }
       }
       connection.Close();
       return clientPhotosList;
    }
}