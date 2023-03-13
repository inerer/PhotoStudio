using System;

namespace PhotoStudio.Models.DataBase;

public class Request
{
    public Request()
    {
        Client = new Client();
    }

    public int Id { get; set; }
    public int ClientId { get; set; }
    public Client? Client { get; set; }
    public DateTime? RequestTimestamp { get; set; }
}