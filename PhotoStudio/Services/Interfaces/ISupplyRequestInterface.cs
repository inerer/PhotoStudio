using System.Collections.Generic;
using PhotoStudio.Models.DataBase;

namespace PhotoStudio.Services.Interfaces;

public interface ISupplyRequestInterface
{
    public SupplyRequest GetSupplyRequest(int id);
    public SupplyRequest AddSupplyRequest(SupplyRequest supplyRequest);
    public bool EditSupplyRequest(SupplyRequest supplyRequest);
    public bool DeleteSupplyRequest(int id);
    public List<SupplyRequest> GetAllSupplyRequests(SupplyRequest supplyRequest);
}