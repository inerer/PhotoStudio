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
    private readonly WorkerRepository? _workerRepository;

    public WorkerService()
    {
        _workerRepository = new WorkerRepository();
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
        return _workerRepository.AddWorker(worker);
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