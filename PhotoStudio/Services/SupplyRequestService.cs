using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using PhotoStudio.DataBase.Repositories;
using PhotoStudio.Models.DataBase;

namespace PhotoStudio.Services.Interfaces;

public class SupplyRequestService:ISupplyRequestInterface
{
    private readonly string _connectionString;
    private readonly SupplyRequestRepository _supplyRequestRepository;
    
    public SupplyRequestService()
    {
        var configBuilder = new ConfigurationBuilder().AddJsonFile( "C:/Users/arshi/RiderProjects/PhotoStudio/PhotoStudio/appsettings.json").Build();
        var configSection = configBuilder.GetSection("ConnectionStrings");
        _connectionString = configSection["PhotoStudioDB"] ?? null;
        _supplyRequestRepository = new SupplyRequestRepository(_connectionString);
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