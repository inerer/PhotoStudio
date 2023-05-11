using System.Collections.Generic;
using PhotoStudio.DataBase.Repositories;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Services.Interfaces;

namespace PhotoStudio.Services;

public class ClientService:IClientInterface
{
    private readonly ClientRepository _clientRepository;
    
    public ClientService()
    {
        _clientRepository = new ClientRepository();
    }

    public Client GetClient(int id)
    {
        return _clientRepository.GetClient(id);
    }

    public Client AddClient(Client client)
    {
       return _clientRepository.AddClient(client);
    }

    public bool EditClient(Client client)
    {
        throw new System.NotImplementedException();
    }

    public bool DeleteClient(int id)
    {
        throw new System.NotImplementedException();
    }

    public List<Client> GetAllClients(Client client)
    {
        throw new System.NotImplementedException();
    }

    public Client GetClientByIdPersonalInfo(PersonalInfo personalInfo)
    {
        return _clientRepository.GetClientByIdPersonalInfo(personalInfo);
    }
}