using System.Collections.Generic;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Services.Interfaces;

namespace PhotoStudio.Services;

public class PersonalInfoService:IPersonalInfoRepository
{
    public PersonalInfo GetPersonalInfo(int id)
    {
        throw new System.NotImplementedException();
    }

    public PersonalInfo AddPersonalInfo(PersonalInfo personalInfo)
    {
        throw new System.NotImplementedException();
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