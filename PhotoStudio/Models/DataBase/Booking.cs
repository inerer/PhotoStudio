﻿using System;
using PhotoStudio.Models.DataBase.SupplyRequestModels;

namespace PhotoStudio.Models.DataBase;

public class Booking
{
    public Booking()
    {
        Worker = new Worker();
        Request = new Request();
    }

    public int Id { get; set; }
    public Worker? Worker { get; set; }
    public Request? Request { get; set; }
    public decimal? TotalPrice { get; set; }
    public DateTime? OrderTimestamp { get; set; }
}