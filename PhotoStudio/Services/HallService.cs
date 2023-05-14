using System.Collections.Generic;
using PhotoStudio.DataBase.Repositories;
using PhotoStudio.Models.DataBase.SupplyRequestModels;
using PhotoStudio.Services.Interfaces;

namespace PhotoStudio.Services;

public class HallService:IHallInterface
{
    private readonly HallRepository _hallRepository;
    
    public HallService()
    {
        _hallRepository = new HallRepository();
    }

    public Hall GetHall(int id)
    {
        throw new System.NotImplementedException();
    }

    public Hall AddHall(Hall hall)
    {
        return _hallRepository.AddHall(hall);
    }

    public bool EditHall(Hall hall)
    {
        throw new System.NotImplementedException();
    }

    public bool DeleteHall(int id)
    {
        throw new System.NotImplementedException();
    }

    public List<Hall> GetAllHalls(Hall hall)
    {
        throw new System.NotImplementedException();
    }
}