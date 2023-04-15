using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using PhotoStudio.DataBase.Repositories;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Services.Interfaces;

namespace PhotoStudio.Services;

public class PersonalInfoService:IPersonalInfoRepository
{
    private readonly PersonalInfoRepository _personalInfoRepository;
    public PersonalInfoService()
    {
        _personalInfoRepository = new PersonalInfoRepository();
    }

    public PersonalInfo GetPersonalInfo(int id)
    {
        return _personalInfoRepository.GetPersonalInfo(id);
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

    public PersonalInfo CheckPersonalInfoByLastNameAndFirstName(PersonalInfo personalInfo)
    {
        return _personalInfoRepository.CheckPersonalInfoByLastNameAndFirstName(personalInfo);
    }
}