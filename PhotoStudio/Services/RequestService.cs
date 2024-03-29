﻿using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using PhotoStudio.DataBase.Repositories;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Models.DataBase.SupplyRequestModels;
using PhotoStudio.Services.Interfaces;

namespace PhotoStudio.Services;

public class RequestService:IRequestInterface
{
    private readonly RequestRepository _requestRepository;

    public RequestService()
    {
        _requestRepository = new RequestRepository();
    }

    public Request GetRequest(int id)
    {
        throw new System.NotImplementedException();
    }

    public Request AddRequest(Request request)
    {
        return _requestRepository.AddRequest(request);
    }

    public bool EditRequest(Request request)
    {
        throw new System.NotImplementedException();
    }

    public bool DeleteRequest(int id)
    {
        return _requestRepository.DeleteRequest(id);
    }

    public List<Request> Requests(Request request)
    {
        return _requestRepository.Requests(request);
    }

    public Request GetRequestByClientId(Client client)
    {
        return _requestRepository.GetRequestByClientId(client);
    }
}