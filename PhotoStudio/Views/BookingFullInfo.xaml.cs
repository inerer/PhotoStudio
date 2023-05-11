using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Models.DataBase.SupplyRequestModels;
using PhotoStudio.Services.Interfaces;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace PhotoStudio.Views;

public partial class BookingFullInfo : UserControl
{

    private readonly Booking _booking;
    private readonly SupplyRequest _supplyRequest;
    private Request _request;
    private readonly SupplyRequestService _supplyRequestService;
    public BookingFullInfo(Booking booking)
    {
        _request = new Request();
        _booking = booking;
        _supplyRequest = new SupplyRequest();
        _supplyRequestService = new SupplyRequestService();
        InitializeComponent();
        RenderListView();
    }

    private void RenderListView()
    {
        if (_booking.Request != null) _request = _booking.Request;
        FullInfoBookingListView.ItemsSource = _supplyRequestService.GelSupplyRequestByRequestId(_request);
    }

    private void GenerateWordButton_OnClick(object sender, RoutedEventArgs e)
    {
        string path = @"C:\Users\arshi\Downloads\Boba.docx";
        string pathDocument = AppDomain.CurrentDomain.BaseDirectory + "example.docx";
 
        // создаём документ
        DocX document = DocX.Create(path);
 
        // Вставляем параграф и указываем текст
        document.InsertParagraph($@"Заказ пользователя {_booking.Request.Client.PersonalInfo.FullName}").
            // устанавливаем шрифт
            Font("Calibri").
            // устанавливаем размер шрифта
            FontSize(36).
            // устанавливаем цвет
            Color(Color.Navy).
            // делаем текст жирным
            Bold().
            // устанавливаем интервал между символами
            Spacing(15).
            // выравниваем текст по центру
            Alignment = Alignment.center;
 
        // сохраняем документ
        document.Save();
        
    }
}