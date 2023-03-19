using System;

namespace PhotoStudio.Models.DataBase.SupplyRequestModels;

public class Supply
{
    public Supply()
    {
        TypeSupply = new TypeSupply();
        Rent = new Rent();
    }

    public int Id { get; set; }
    public TypeSupply? TypeSupply { get; set; }
    public Rent? Rent { get; set; }
    public string? Name { get; set; }
    public decimal? Price { get; set; }
    public string? Description { get; set; }
    public DateTime? SupplyTimestamp { get; set; }
}