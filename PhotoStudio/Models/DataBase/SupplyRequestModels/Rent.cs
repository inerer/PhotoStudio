namespace PhotoStudio.Models.DataBase;

public class Rent
{
    public Rent()
    {
        Hall = new Hall();
    }

    public int Id { get; set; }
    public decimal? PriceHour { get; set; }
    public Hall? Hall { get; set; }
}