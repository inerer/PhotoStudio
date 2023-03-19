using System.Collections.Generic;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Models.DataBase.SupplyRequestModels;

namespace PhotoStudio.Services;

public interface IRentInterface
{
    public Rent GetRent(int id);
    public Rent AddRent(Rent rent);
    public bool EditRent(Rent rent);
    public bool DeleteRent(int id);
    public List<Rent> GetAllRents(Rent rent);
}