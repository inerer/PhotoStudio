using System.Collections.Generic;
using PhotoStudio.DataBase.Repositories;
using PhotoStudio.Models.DataBase.SupplyRequestModels;
using PhotoStudio.Services.Interfaces;

namespace PhotoStudio.Services;

public class TypeSupplyService:ITypeSupplyInterface
{
    private readonly TypeSupplyRepository _typeSupplyRepository;
    
    public TypeSupplyService()
    {
        _typeSupplyRepository = new TypeSupplyRepository();
    }

    public TypeSupply GetTypeSupply(int id)
    {
        throw new System.NotImplementedException();
    }

    public TypeSupply AddTypeSupply(TypeSupply typeSupply)
    {
        throw new System.NotImplementedException();
    }

    public bool EditTypeSupply(TypeSupply typeSupply)
    {
        throw new System.NotImplementedException();
    }

    public bool DeleteTypeSupply(int id)
    {
        throw new System.NotImplementedException();
    }

    public List<TypeSupply> GetAllTypeSupplies(TypeSupply typeSupply)
    {
        return _typeSupplyRepository.GetAllTypeSupplies(typeSupply);
    }
}