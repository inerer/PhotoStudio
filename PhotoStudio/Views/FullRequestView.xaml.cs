﻿using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Models.DataBase.SupplyRequestModels;
using PhotoStudio.Services;
using PhotoStudio.Services.Interfaces;

namespace PhotoStudio.Views;

public partial class FullRequestView : UserControl
{
    private readonly Request _request;
    private readonly SupplyRequestService _supplyRequestService;
    private readonly Worker _worker;
    private readonly BookingService _bookingService;
    private readonly Booking _booking;
    public FullRequestView(Request request, Worker worker)
    {
        _request = request;
        _worker = worker;
        _booking = new Booking();
        _bookingService = new BookingService();
        _supplyRequestService = new SupplyRequestService();

        InitializeComponent();
        RenderServiceRequestInfo();
        RenderTotalPriceLabel();
    }    
    
    private void RenderServiceRequestInfo()
    {
        FullInfoListView.ItemsSource = _supplyRequestService.GelSupplyRequestByRequestId(_request);
    }

    private void RenderTotalPriceLabel()
    {
        decimal totalPrice = 0;
        
        foreach (var supplyRequest in _supplyRequestService.GelSupplyRequestByRequestId(_request))
        {
           totalPrice += (decimal)supplyRequest.Supply.Price;
        }

        TotalPriceLabel.Content = totalPrice;
        _booking.TotalPrice = totalPrice;
    }

    private void AddOrderButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            BookingFiling();
            _bookingService.AddBooking(_booking);
            SendMessage(_booking);
            MessageBox.Show("Заказ проведен");
        }
        catch
        {
            MessageBox.Show("Ошибка добавления заказа!");
        }
    }

    private void BookingFiling()
    {
        _booking.Request = _request;
        _booking.Worker = _worker;
    }

    public async Task SendMessage(Booking booking)
    {
        EmailService emailService = new();
        await emailService.SendEmailAsync(booking.Request.Client.PersonalInfo.Email, "Оформление заказа",
            "Поздравляю вы оформили заказ!!!!!!!!");
        
    }
}