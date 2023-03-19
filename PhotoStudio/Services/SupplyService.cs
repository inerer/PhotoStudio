using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using PhotoStudio.DataBase.Repositories;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Models.DataBase.SupplyRequestModels;
using PhotoStudio.Services.Interfaces;

namespace PhotoStudio.Services;

public class SupplyService:ISupplyInterface
{
    private readonly SupplyRepository _supplyRepository;

    public SupplyService()
    {
        _supplyRepository = new SupplyRepository();
    }

    public Supply GetSupply(int id)
    {
        throw new System.NotImplementedException();
    }

    public Supply AddSupply(Supply supply)
    {
        throw new System.NotImplementedException();
    }

    public bool EditSupply(Supply supply)
    {
        throw new System.NotImplementedException();
    }

    public bool DeleteSupply(int id)
    {
        throw new System.NotImplementedException();
    }

    public List<Supply> GetAllSupplies(Supply supply)
    {
        return _supplyRepository.GetAllSupplies(supply);
    }
}