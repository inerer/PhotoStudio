using System.Collections.Generic;
using System.Configuration;
using System.Windows.Documents;
using Microsoft.Extensions.Configuration;
using PhotoStudio.DataBase.Repositories;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Services.Interfaces;

namespace PhotoStudio.Services;

public class WorkerService:IWorkerInterface
{
    private readonly string _connectionString;
    private readonly WorkerRepository? _workerRepository;

    public WorkerService()
    {
        
        var configBuilder = new ConfigurationBuilder().AddJsonFile( "C:/Users/arshi/RiderProjects/PhotoStudio/PhotoStudio/appsettings.json").Build();
        var configSection = configBuilder.GetSection("ConnectionStrings");
        _connectionString = configSection["PhotoStudioDB"] ?? null;
        _workerRepository = new WorkerRepository(_connectionString);
    }

    public bool Auth(string login, string password)
    {
        return GetWorkerByLogin(login).Password == password;
    }

    public Worker GetWorker(int id)
    {
        return null;
    }

    public Worker AddWorker(Worker worker)
    {
        return null;
    }

    public bool EditWorker(Worker worker)
    {
        return false;
    }

    public bool DeleteWorker(int id)
    {
        return false;
    }

    public List<Worker> GetAllWorkers(Worker worker)
    {
        return _workerRepository.GetAllWorkers(worker);
    }

    public Worker GetWorkerByLogin(string login)
    {
        return _workerRepository.GetWorkerByLogin(login);
    }
}