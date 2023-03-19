using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using PhotoStudio.DataBase.Repositories;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Models.DataBase.SupplyRequestModels;

namespace PhotoStudio.Services.Interfaces;

public class SupplyRequestService:ISupplyRequestInterface
{
    private readonly SupplyRequestRepository _supplyRequestRepository;
    
    public SupplyRequestService()
    {
        _supplyRequestRepository = new SupplyRequestRepository();
    }

    public SupplyRequest GetSupplyRequest(int id)
    {
        throw new System.NotImplementedException();
    }

    public SupplyRequest AddSupplyRequest(SupplyRequest supplyRequest)
    {
        throw new System.NotImplementedException();
    }

    public bool EditSupplyRequest(SupplyRequest supplyRequest)
    {
        throw new System.NotImplementedException();
    }

    public bool DeleteSupplyRequest(int id)
    {
        throw new System.NotImplementedException();
    }

    public List<SupplyRequest> GetAllSupplyRequests(SupplyRequest supplyRequest)
    {
        throw new System.NotImplementedException();
    }

    public List<SupplyRequest> GelSupplyRequestByRequestId(Request request)
    {
        return _supplyRequestRepository.GelSupplyRequestByRequestId(request);
    }
}