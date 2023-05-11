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
        document.InsertParagraph($"Договор пользователя:{_booking.Request.Client.PersonalInfo.FullName}").
            // устанавливаем шрифт
            Font("Times New Roman").
            // устанавливаем размер шрифта
            FontSize(16).
            // устанавливаем цвет
            Color(Color.Black).
            // делаем текст жирным
            Bold().
            // выравниваем текст по центру
            Alignment = Alignment.center;

        document.InsertParagraph("Заказанные услуги:").
            Font("Times New Roman").
            FontSize(16).
            Alignment = Alignment.left;

        Paragraph paragraph = document.InsertParagraph();
        paragraph.Alignment = Alignment.left;
        foreach (var supplyRequest in _supplyRequestService.GelSupplyRequestByRequestId(_request))
        {
            paragraph.AppendLine($"Имя: {supplyRequest.Supply?.Name}, Цена: {supplyRequest.Supply?.Price} рублей").
                FontSize(14).
                Font("Times New Roman");
            paragraph.AppendLine();
        }

        Paragraph paragraph1 = document.InsertParagraph();
        paragraph1.Alignment = Alignment.right;
        decimal totalPrice = 0;
        
        foreach (var supplyRequest in _supplyRequestService.GelSupplyRequestByRequestId(_request))
        {
            if (supplyRequest.Supply != null) totalPrice += (decimal)supplyRequest.Supply.Price;
        }

        paragraph1.AppendLine($"Итоговая цена {totalPrice}").FontSize(14).Font("Times New Roman");
 
        // сохраняем документ
        document.Save();
        
    }
}