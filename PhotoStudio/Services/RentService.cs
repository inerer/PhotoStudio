using System.Collections.Generic;
using PhotoStudio.DataBase.Repositories;
using PhotoStudio.Models.DataBase.SupplyRequestModels;

namespace PhotoStudio.Services;

public class RentService : IRentInterface
{
    private RentRepository _rentRepository;
    
    public RentService()
    {
        _rentRepository = new RentRepository();
    }

    public Rent GetRent(int id)
    {
        throw new System.NotImplementedException();
    }

    public Rent AddRent(Rent rent)
    {
        throw new System.NotImplementedException();
    }

    public bool EditRent(Rent rent)
    {
        throw new System.NotImplementedException();
    }

    public bool DeleteRent(int id)
    {
        throw new System.NotImplementedException();
    }

    public List<Rent> GetAllRents(Rent rent)
    {
        return _rentRepository.GetAllRents(rent);
    }
}