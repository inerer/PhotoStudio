using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using PhotoStudio.DataBase.Repositories;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Services.Interfaces;

namespace PhotoStudio.Services;

public class PersonalInfoService:IPersonalInfoRepository
{
    private readonly string _connectionString;
    private readonly PersonalInfoRepository _personalInfoRepository;
    public PersonalInfoService()
    {
        var configBuilder = new ConfigurationBuilder().AddJsonFile( "C:/Users/arshi/RiderProjects/PhotoStudio/PhotoStudio/appsettings.json").Build();
        var configSection = configBuilder.GetSection("ConnectionStrings");
        _connectionString = configSection["PhotoStudioDB"] ?? null;
        _personalInfoRepository = new PersonalInfoRepository(_connectionString);
    }

    public PersonalInfo GetPersonalInfo(int id)
    {
        throw new System.NotImplementedException();
    }

    public int AddPersonalInfo(PersonalInfo personalInfo)
    {
        return _personalInfoRepository.AddPersonalInfo(personalInfo);
    }

    public bool EditPersonalInfo(PersonalInfo personalInfo)
    {
        throw new System.NotImplementedException();
    }

    public bool DeletePersonalInfo(int id)
    {
        throw new System.NotImplementedException();
    }

    public List<PersonalInfo> GelAllPersonalInfos(PersonalInfo personalInfo)
    {
        throw new System.NotImplementedException();
    }
}