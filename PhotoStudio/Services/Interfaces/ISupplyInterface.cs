using System.Collections.Generic;
using System.Windows.Documents;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Models.DataBase.SupplyRequestModels;

namespace PhotoStudio.Services.Interfaces;

public interface ISupplyInterface
{
    public Supply GetSupply(int id);
    public Supply AddSupply(Supply supply);
    public Supply AddSupplyForRent(Supply supply);
    public bool EditSupply(Supply supply);
    public bool EditSupplyForRent(Supply supply);
    public bool DeleteSupply(int id);
    public List<Supply> GetAllSupplies(Supply supply);
    public List<Supply> GetAllSuppliesDontRent(Supply supply);
}