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
        return _supplyRepository.AddSupply(supply);
    }

    public Supply AddSupplyForRent(Supply supply)
    {
        return _supplyRepository.AddSupplyForRent(supply);
    }

    public bool EditSupply(Supply supply)
    {
        return _supplyRepository.EditSupply(supply);
    }

    public bool EditSupplyForRent(Supply supply)
    {
        return _supplyRepository.EditSupplyForRent(supply);
    }

    public bool DeleteSupply(int id)
    {
        return _supplyRepository.DeleteSupply(id);
    }

    public List<Supply> GetAllSupplies(Supply supply)
    {
        return _supplyRepository.GetAllSupplies(supply);
    }

    public List<Supply> GetAllSuppliesDontRent(Supply supply)
    {
        return _supplyRepository.GetAllSuppliesDontRent(supply);
    }
}