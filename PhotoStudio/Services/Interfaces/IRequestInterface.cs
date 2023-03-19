using System.Collections.Generic;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Models.DataBase.SupplyRequestModels;

namespace PhotoStudio.Services.Interfaces;

public interface IRequestInterface
{
    public Request GetRequest(int id);
    public Request AddRequest(Request request);
    public bool EditRequest(Request request);
    public bool DeleteRequest(int id);
    public List<Request> Requests(Request request);
}