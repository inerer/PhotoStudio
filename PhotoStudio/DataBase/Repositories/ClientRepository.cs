using Npgsql;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Services.Interfaces;

namespace PhotoStudio.DataBase.Repositories;

public class ClientRepository:IClientInterface
{
    private readonly string _connectionString;
    private NpgsqlConnection connection;
    
    public ClientRepository(string connectionString)
    {
        _connectionString = connectionString;
        connection = new NpgsqlConnection(_connectionString);
    }
    
    public Client GetClient(int id)
    {
        connection.Open();
        Client client = new Client();
        string query =
            @"select * from client join personal_info on client.id_personal_info = personal_info.id_personal_info where id_client = $1";
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
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
        connection.Close();
        return client;
    }

    public Client AddClient(Client client)
    {
        throw new System.NotImplementedException();
    }

    public bool EditClient(Client client)
    {
        throw new System.NotImplementedException();
    }

    public bool DeleteClient(int id)
    {
        throw new System.NotImplementedException();
    }

    public Client GetAllClients(Client client)
    {
        throw new System.NotImplementedException();
    }
}