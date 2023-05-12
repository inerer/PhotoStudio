using System.Net.Mail;

namespace PhotoStudio.Models.DataBase;

public class PersonalInfo
{
    public int Id { get; set; }
    public string? LastName { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? Email { get; set; }
    public string? MobilePhone { get; set; }
    public string? FullName => $"{LastName} {FirstName[0]}.{MiddleName[0]}.";
}