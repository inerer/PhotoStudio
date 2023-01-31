using System.Collections.Generic;
using PhotoStudio.Models.DataBase;

namespace PhotoStudio.Services.Interfaces;

public interface IEquipmentInterface
{
    public Equipment GetEquipment(int id);
    public Equipment AddEquipment(Equipment equipment);
    public bool EditEquipment(Equipment equipment);
    public bool DeleteEquipment(int id);
    public List<Equipment> GetAllEquipments(Equipment equipment);
}