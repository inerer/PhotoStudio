namespace PhotoStudio.Models.DataBase;

public class SupplyRequest
{
    public int Id { get; set; }
    public Supply Supply { get; set; }
    public Request Request { get; set; }
}