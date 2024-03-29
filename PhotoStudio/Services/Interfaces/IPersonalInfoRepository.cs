﻿using System.Collections.Generic;
using PhotoStudio.Models.DataBase;

namespace PhotoStudio.Services.Interfaces;

public interface IPersonalInfoRepository
{
    public PersonalInfo GetPersonalInfo(int id);
    public int AddPersonalInfo(PersonalInfo personalInfo);
    public bool EditPersonalInfo(PersonalInfo personalInfo);
    public bool DeletePersonalInfo(int id);
    public List<PersonalInfo> GelAllPersonalInfos(PersonalInfo personalInfo);
    public PersonalInfo CheckPersonalInfoByLastNameAndFirstName(PersonalInfo personalInfo);
    
}