using PhotoStudio.Models.DataBase.SupplyRequestModels;

namespace PhotoStudio.Models.DataBase;

public class EquipmentRent
{
    public int Id { get; set; }
    public Equipment? Equipment { get; set; }
    public Rent? Rent { get; set; }
}