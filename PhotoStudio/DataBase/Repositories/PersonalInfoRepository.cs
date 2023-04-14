using System;
using System.Collections.Generic;
using System.Windows.Documents;
using Npgsql;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Services.Interfaces;

namespace PhotoStudio.DataBase.Repositories;

public class PersonalInfoRepository:RepositoryBase ,IPersonalInfoRepository
{
    private readonly NpgsqlConnection _connection;

    public PersonalInfoRepository()
    {
        
        _connection = GetConnection();
    }

    public PersonalInfo GetPersonalInfo(int id)
    {
        _connection.Open();
        PersonalInfo personalInfo = new();
        string query = "Select * from personal_info where id_personal_info=($1)";
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
                    personalInfo.Id = Convert.ToInt32(reader["id_personal_info"]);
                    personalInfo.LastName = reader["last_name"].ToString();
                    personalInfo.FirstName = reader["first_name"].ToString();
                    personalInfo.MiddleName = reader["middle_name"].ToString();
                    personalInfo.Email = reader["email"].ToString();
                    personalInfo.MobilePhone = reader["mobile_phone"].ToString();
                }
            }
        }
        _connection.Close();
        return personalInfo;
    }

    

    public int AddPersonalInfo(PersonalInfo personalInfo)
    {
        _connection.Open();
        string query =
            "insert into personal_info(last_name, first_name, middle_name, email, mobile_phone) values ($1,$2,$3,$4,$5) returning id_personal_info";
        NpgsqlCommand command = new(query, _connection)
        {
            Parameters =
            {
                new NpgsqlParameter() { Value = personalInfo.LastName },
                new NpgsqlParameter() { Value = personalInfo.FirstName },
                new NpgsqlParameter() { Value = personalInfo.MiddleName },
                new NpgsqlParameter() { Value = personalInfo.Email },
                new NpgsqlParameter() { Value = personalInfo.MobilePhone }
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
                    personalInfo.Id = Convert.ToInt32(reader["id_personal_info"]);
                }
            }
        }
        
        try
        {
            return personalInfo.Id;
        }
        catch (Exception e)
        {
            return 0;
        }
        finally
        {
            _connection.Close();  
        }
    }

    public bool EditPersonalInfo(PersonalInfo personalInfo)
    {
        _connection.Open();
        string query =
            "update personal_info set last_name=($1), first_name=($2), middle_name=($3), email = ($4), mobile_phone=($5) where id_personal_info=($6)";
        NpgsqlCommand command = new(query, _connection)
        {
            Parameters =
            {

                new NpgsqlParameter() { Value = personalInfo.LastName },
                new NpgsqlParameter() { Value = personalInfo.FirstName },
                new NpgsqlParameter() { Value = personalInfo.MiddleName },
                new NpgsqlParameter() { Value = personalInfo.Email },
                new NpgsqlParameter() { Value = personalInfo.MobilePhone },
                new NpgsqlParameter() { Value = personalInfo.Id }
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

    public bool DeletePersonalInfo(int id)
    {
        _connection.Open();
        string query = "delete from personal_info where id_personal_info = ($1) ";
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

    public List<PersonalInfo> GelAllPersonalInfos(PersonalInfo personalInfo)
    {
        _connection.Open();
        List<PersonalInfo> personalInfos = new();
        string query = "Select * from personal_info";
        NpgsqlCommand command = new(query, _connection);
        using (command)
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    personalInfo.Id = Convert.ToInt32(reader["id_personal_info"]);
                    personalInfo.LastName = reader["last_name"].ToString();
                    personalInfo.FirstName = reader["first_name"].ToString();
                    personalInfo.MiddleName = reader["middle_name"].ToString();
                    personalInfo.Email = reader["email"].ToString();
                    personalInfo.MobilePhone = reader["mobile_phone"].ToString();
                    personalInfos.Add(personalInfo);
                }
            }
        }
        _connection.Close();
        return personalInfos;
    }

    public bool CheckPersonalInfoByLastNameAndFirstName(PersonalInfo personalInfo)
    {
        _connection.Open();
        string query = "select * from personal_info where ($1 AND $2)";
        NpgsqlCommand command = new(query, _connection)
        {
            Parameters =
            {
                new NpgsqlParameter() { Value = personalInfo.LastName },
                new NpgsqlParameter() { Value = personalInfo.FirstName }
            }
        };
        
    }
}