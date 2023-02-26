using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using PhotoStudio.DataBase.Repositories;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Services.Interfaces;

namespace PhotoStudio.Services;

public class RequestService:IRequestInterface
{
    private readonly string _connectionString;
    private readonly RequestRepository _requestRepository;

    public RequestService()
    {
        var configBuilder = new ConfigurationBuilder().AddJsonFile( "C:/Users/arshi/RiderProjects/PhotoStudio/PhotoStudio/appsettings.json").Build();
        var configSection = configBuilder.GetSection("ConnectionStrings");
        _connectionString = configSection["PhotoStudioDB"] ?? null;
        _requestRepository = new RequestRepository(_connectionString);
    }

    public Request GetRequest(int id)
    {
        throw new System.NotImplementedException();
    }

    public Request AddRequest(Request request)
    {
        throw new System.NotImplementedException();
    }

    public bool EditRequest(Request request)
    {
        throw new System.NotImplementedException();
    }

    public bool DeleteRequest(int id)
    {
        throw new System.NotImplementedException();
    }

    public List<Request> Requests(Request request)
    {
        return _requestRepository.Requests(request);
    }
}