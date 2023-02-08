namespace PhotoStudio.Models.DataBase;

public class Rent
{
    public int Id { get; set; }
    public decimal? PriceHour { get; set; }
    public Hall? Hall { get; set; }
}