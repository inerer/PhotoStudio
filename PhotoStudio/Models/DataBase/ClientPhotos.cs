namespace PhotoStudio.Models.DataBase;

public class ClientPhotos
{
    public int Id { get; set; }
    public string Photo { get; set; }
    public Request Request { get; set; }
}