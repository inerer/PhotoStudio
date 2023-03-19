using System.Collections.Generic;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Models.DataBase.SupplyRequestModels;

namespace PhotoStudio.Services.Interfaces;

public interface ITypeSupplyInterface
{
    public TypeSupply GetTypeSupply(int id);
    public TypeSupply AddTypeSupply(TypeSupply typeSupply);
    public bool EditTypeSupply(TypeSupply typeSupply);
    public bool DeleteTypeSupply(int id);
    public List<TypeSupply> GetAllTypeSupplies(TypeSupply typeSupply);
}