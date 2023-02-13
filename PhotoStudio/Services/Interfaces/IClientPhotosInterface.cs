using System.Collections.Generic;
using PhotoStudio.Models.DataBase;

namespace PhotoStudio.Services.Interfaces;

public interface IClientPhotosInterface
{
    public ClientPhotos GetClientPhotos(int id);
    public List<ClientPhotos> GetPhotoForRequest(int id);
    public ClientPhotos AddClientPhotos(ClientPhotos clientPhotos);
    public bool EditClientPhotos(ClientPhotos clientPhotos);
    public bool DeleteClientPhotos(int id);
    public List<ClientPhotos> GetAllClientPhotosList(ClientPhotos clientPhotos);

}