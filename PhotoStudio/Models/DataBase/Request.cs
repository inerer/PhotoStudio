using System;

namespace PhotoStudio.Models.DataBase;

public class Request
{
    public int Id { get; set; }
    public Client Client { get; set; }
    public DateTime RequestTimestamp { get; set; }
}