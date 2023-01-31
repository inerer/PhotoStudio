using System.Collections.Generic;
using System.Windows.Documents;
using PhotoStudio.Models.DataBase;

namespace PhotoStudio.Services.Interfaces;

public interface IHallInterface
{
    public Hall GetHall(int id);
    public Hall AddHall(Hall hall);
    public bool EditHall(Hall hall);
    public bool DeleteHall(int id);
    public List<Hall> GetAllHalls(Hall hall);
}