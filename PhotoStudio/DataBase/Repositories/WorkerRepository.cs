using System;
using System.Collections.Generic;
using Npgsql;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Services.Interfaces;

namespace PhotoStudio.DataBase.Repositories;

public class WorkerRepository:IWorkerInterface
{
    private readonly string _connectionString;
    private NpgsqlConnection connection;

    public WorkerRepository(string connectionString)
    {
        _connectionString = connectionString;
        connection = new NpgsqlConnection(_connectionString);
    }

    public Worker GetWorker(int id)
    {
        connection.Open();
        Worker worker = new();
        string query =
            "select * from worker join role r on worker.id_role = r.id_role join personal_info pi on worker.id_personal_info = pi.id_personal_info where id_worker =($1)";
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
                    worker.Id = Convert.ToInt32(reader["id_worker"]);
                    worker.Role.RoleName = reader["role_name"].ToString();
                    worker.PersonalInfo.LastName = reader["last_name"].ToString();
                    worker.PersonalInfo.FirstName = reader["first_name"].ToString();
                    worker.PersonalInfo.MiddleName = reader["middle_name"].ToString();
                    worker.PersonalInfo.Email = reader["email"].ToString();
                    worker.PersonalInfo.MobilePhone = reader["mobile_phone"].ToString();
                    worker.Login = reader["login"].ToString();
                    worker.Password = reader["password"].ToString();

                }
            }
        }
        connection.Close();
        return worker;
    }

    public Worker AddWorker(Worker worker)
    {
        connection.Open();
        string query = "insert into worker(id_role, id_personal_info, login, password) values ($1, $2, $3, $4)";
        NpgsqlCommand command = new(query, connection)
        {
            Parameters =
            {
                new NpgsqlParameter() { Value = worker.Role.Id },
                new NpgsqlParameter() { Value = worker.PersonalInfo.Id },
                new NpgsqlParameter() { Value = worker.Login },
                new NpgsqlParameter() { Value = worker.Password }
            }
        };
        try
        {
            command.ExecuteNonQuery();
            return worker;
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

    public bool EditWorker(Worker worker)
    {
        connection.Open();
        string query =
            "update worker set id_role = ($1), id_personal_info = ($2), login = ($3), password = ($4) where id_worker = ($5)";
        NpgsqlCommand command = new(query, connection)
        {
            Parameters =
            {
                new NpgsqlParameter() { Value = worker.Role.Id },
                new NpgsqlParameter() { Value = worker.PersonalInfo.Id },
                new NpgsqlParameter() { Value = worker.Login },
                new NpgsqlParameter() { Value = worker.Password },
                new NpgsqlParameter() { Value = worker.Id }
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

    public bool DeleteWorker(int id)
    {
        connection.Open();
        string query = "delete from worker where id_worker = ($1)";
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

    public List<Worker> GetAllWorkers(Worker worker)
    {
        connection.Open();
        List<Worker> workers = new();
        string query =
            "select * from worker join role r on worker.id_role = r.id_role join personal_info pi on worker.id_personal_info = pi.id_personal_info";
        NpgsqlCommand command = new(query, connection);
        using (command)
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    worker.Id = Convert.ToInt32(reader["id_worker"]);
                    worker.Role.RoleName = reader["role_name"].ToString();
                    worker.PersonalInfo.LastName = reader["last_name"].ToString();
                    worker.PersonalInfo.FirstName = reader["first_name"].ToString();
                    worker.PersonalInfo.MiddleName = reader["middle_name"].ToString();
                    worker.PersonalInfo.Email = reader["email"].ToString();
                    worker.PersonalInfo.MobilePhone = reader["mobile_phone"].ToString();
                    worker.Login = reader["login"].ToString();
                    worker.Password = reader["password"].ToString();
                    workers.Add(worker);
                }
            }
        }
        connection.Close();
        return workers;
    }

    public Worker GetWorkerByLogin(string login)
    {
        connection.Open();
        Worker worker = new();
        string query =
            "select * from worker join role r on worker.id_role = r.id_role join personal_info pi on worker.id_personal_info = pi.id_personal_info where login =($1)";
        NpgsqlCommand command = new(query, connection)
        {
            Parameters = { new NpgsqlParameter() { Value = login } }
        };
        using (command)
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    worker.Id = Convert.ToInt32(reader["id_worker"]);
                    worker.Role.Id = Convert.ToInt32(reader["id_role"]);
                    worker.Role.RoleName = reader["role_name"].ToString();
                    worker.PersonalInfo.LastName = reader["last_name"].ToString();
                    worker.PersonalInfo.FirstName = reader["first_name"].ToString();
                    worker.PersonalInfo.MiddleName = reader["middle_name"].ToString();
                    worker.PersonalInfo.Email = reader["email"].ToString();
                    worker.PersonalInfo.MobilePhone = reader["mobile_phone"].ToString();
                    worker.Login = reader["login"].ToString();
                    worker.Password = reader["password"].ToString();
                }
            }
        }
        connection.Close();
        return worker;
    }
}