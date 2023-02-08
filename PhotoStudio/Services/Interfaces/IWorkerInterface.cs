using System.Collections.Generic;
using PhotoStudio.Models.DataBase;

namespace PhotoStudio.Services.Interfaces;

public interface IWorkerInterface
{
    public Worker GetWorker(int id);
    public Worker AddWorker(Worker worker);
    public bool EditWorker(Worker worker);
    public bool DeleteWorker(int id);
    public List<Worker> GetAllWorkers(Worker worker);
    public Worker GetWorkerByLogin(string login);
}