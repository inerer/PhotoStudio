using System.Collections.Generic;
using System.Windows.Documents;
using PhotoStudio.Models.DataBase;

namespace PhotoStudio.Services.Interfaces;

public interface IEquipmentRentInterface
{
    public EquipmentRent GetEquipmentRent(int id);
    public EquipmentRent AddEquipmentRent(EquipmentRent equipmentRent);
    public bool EditEquipmentRent(EquipmentRent equipmentRent);
    public bool DeleteEquipmentRent(EquipmentRent equipmentRent);
    public List<EquipmentRent> GelAllEquipmentRents(EquipmentRent equipmentRent);
}