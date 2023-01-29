using PhotoStudio.Models.DataBase;

namespace PhotoStudio.Services.Interfaces;

public interface IClientInterface
{
    public Client GetClient(int id);
    public Client AddClient(Client client);
    public bool EditClient(Client client);
    public bool DeleteClient(int id);
    public Client GetAllClients(Client client);
}