using System.Collections.Generic;
using PhotoStudio.DataBase.Repositories;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Services.Interfaces;

namespace PhotoStudio.Services;

public class ClientPhotosService:IClientPhotosInterface
{
    private ClientPhotosRepository _clientPhotosRepository;

    public ClientPhotosService()
    {
        _clientPhotosRepository = new ClientPhotosRepository();
    }

    public ClientPhotos GetClientPhotos(int id)
    {
        throw new System.NotImplementedException();
    }

    public List<ClientPhotos> GetPhotoForRequest(int id)
    {
        throw new System.NotImplementedException();
    }

    public ClientPhotos AddClientPhotos(ClientPhotos clientPhotos)
    {
        return _clientPhotosRepository.AddClientPhotos(clientPhotos);
    }

    public bool EditClientPhotos(ClientPhotos clientPhotos)
    {
        throw new System.NotImplementedException();
    }

    public bool DeleteClientPhotos(int id)
    {
        throw new System.NotImplementedException();
    }

    public List<ClientPhotos> GetAllClientPhotosList(ClientPhotos clientPhotos)
    {
        throw new System.NotImplementedException();
    }
}