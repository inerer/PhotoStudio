namespace PhotoStudio.Models.DataBase;

public class Worker
{
    public Worker()
    {
        PersonalInfo = new PersonalInfo();
        Role = new Role();
    }

    public int Id { get; set; }
    
    public int RoleId { get; set; }
    public Role? Role { get; set; }
    
    public int PersonalInfoId { get; set; }
    public PersonalInfo? PersonalInfo { get; set; }
    public string? Login { get; set; }
    public string? Password { get; set; }
}