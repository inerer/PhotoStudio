using System;
using System.Collections.Generic;
using Npgsql;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Services.Interfaces;

namespace PhotoStudio.DataBase.Repositories;

public class WorkerRepository:RepositoryBase ,IWorkerInterface
{
    private readonly RoleRepository _roleRepository;
    private readonly PersonalInfoRepository _personalInfo;
    private readonly NpgsqlConnection _connection;
    private PersonalInfoRepository _personalInfoRepository;

    public WorkerRepository()
    {
        
        _connection = GetConnection();
        _roleRepository = new RoleRepository();
        _personalInfo = new PersonalInfoRepository();
    }

    public Worker GetWorker(int id)
    {
        _connection.Open();
        Worker worker = new();
        string query =
            "select * from worker join role r on worker.id_role = r.id_role join personal_info pi on worker.id_personal_info = pi.id_personal_info where id_worker =($1)";
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
                    worker.Id = Convert.ToInt32(reader["id_worker"]);
                    worker.Login = reader["login"].ToString();
                    worker.Password = reader["password"].ToString();
                }
            }
        }
        _connection.Close();
        worker.Role = _roleRepository.GetRole(worker.Id);
        worker.PersonalInfo = _personalInfo.GetPersonalInfo(worker.Id);
        return worker;
    }

    public Worker AddWorker(Worker worker)
    {
        _connection.Open();
        string query = "insert into worker(id_role, id_personal_info, login, password) values ($1, $2, $3, $4)";
        NpgsqlCommand command = new(query, _connection)
        {
            Parameters =
            {
                new NpgsqlParameter() { Value = worker.Role.Id },
                new NpgsqlParameter() { Value = worker.PersonalInfoId },
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
            _connection.Close();  
        }     
    }

    public bool EditWorker(Worker worker)
    {
        _connection.Open();
        string query =
            "update worker set id_role = ($1), id_personal_info = ($2), login = ($3), password = ($4) where id_worker = ($5)";
        NpgsqlCommand command = new(query, _connection)
        {
            Parameters =
            {
                new NpgsqlParameter() { Value = worker.RoleId },
                new NpgsqlParameter() { Value = worker.PersonalInfoId },
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
            _connection.Close();  
        }     
    }

    public bool DeleteWorker(int id)
    {
        _connection.Open();
        string query = "delete from worker where id_worker = ($1)";
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

    public List<Worker> GetAllWorkers(Worker worker)
    {
        _connection.Open();
        List<Worker> workers = new();
        string query =
            "select * from worker join role r on worker.id_role = r.id_role join personal_info pi on worker.id_personal_info = pi.id_personal_info";
        NpgsqlCommand command = new(query, _connection);
        using (command)
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    worker.Id = Convert.ToInt32(reader["id_worker"]);
                    worker.RoleId = Convert.ToInt32(reader["id_role"]);
                    worker.PersonalInfoId = Convert.ToInt32(reader["id_personal_info"]);
                    worker.Login = reader["login"].ToString();
                    worker.Password = reader["password"].ToString();
                    workers.Add(worker);
                }
            }
        }
        _connection.Close();
        foreach (var work in workers)
        {
            worker.PersonalInfo = _personalInfo.GetPersonalInfo(worker.PersonalInfoId);
            worker.Role = _roleRepository.GetRole(worker.RoleId);
        }
        return workers;
    }

    public Worker GetWorkerByLogin(string login)
    {
        _connection.Open();
        Worker worker = new();
        string query =
            "select * from worker join role r on worker.id_role = r.id_role join personal_info pi on worker.id_personal_info = pi.id_personal_info where login =($1)";
        NpgsqlCommand command = new(query, _connection)
        {
            Parameters = { new NpgsqlParameter() { Value = login } }
        };
        using (command)
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    worker.Id = reader.GetInt32(reader.GetOrdinal("id_worker"));
                    worker.RoleId = reader.GetInt32(reader.GetOrdinal("id_role"));
                    worker.PersonalInfoId = Convert.ToInt32(reader["id_personal_info"]);
                    worker.Login = reader["login"].ToString();
                    worker.Password = reader["password"].ToString();
                }
            }
        }
        worker.Role = _roleRepository.GetRole(worker.RoleId);
        worker.PersonalInfo = _personalInfo.GetPersonalInfo(worker.PersonalInfoId);//
        _connection.Close();
        return worker;
    }
}