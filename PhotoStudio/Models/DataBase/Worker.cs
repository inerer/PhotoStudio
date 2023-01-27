namespace PhotoStudio.Models.DataBase;

public class Worker
{
    public int id { get; set; }
    public Role Role { get; set; }
    public PersonalInfo PersonalInfo { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
}