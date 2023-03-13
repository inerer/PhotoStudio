namespace PhotoStudio.Models.DataBase;

public class SupplyRequest
{
    public SupplyRequest()
    {
        Supply = new Supply();
        Request = new Request();
    }

    public int Id { get; set; }
    public Supply? Supply { get; set; }
    public Request? Request { get; set; }
}