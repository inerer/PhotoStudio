namespace PhotoStudio.Models.DataBase;

public class Client
{
    public Client()
    {
        PersonalInfo = new PersonalInfo();
    }

    public int Id { get; set; }
    public PersonalInfo? PersonalInfo { get; set; }
}