using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Mime;
using System.Windows;
using System.Windows.Controls;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Models.DataBase.SupplyRequestModels;
using PhotoStudio.Services.Interfaces;
using Xceed.Document.NET;
using Xceed.Words.NET;
using Image = System.Drawing.Image;

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
        DocX document = DocX.Create(path);
        string pathDocument = AppDomain.CurrentDomain.BaseDirectory + "example.docx";
        
        // создаём документ
        var image = document.AddImage(@"C:\Users\arshi\Downloads\photo-studio-logo-design_567288-622.png");
        var picture = image.CreatePicture(100, 100);


        Paragraph par = document.InsertParagraph();
        //Paragraph paras = document.InsertParagraph();
        par.AppendPicture(picture).Alignment= Alignment.center;
        par.AppendLine(" ООО ФотоСтудия Аршинов").Bold().Font("Times New Roman").FontSize(14).Alignment=Alignment.center;
        par.AppendLine();
        // Вставляем параграф и указываем текст
        document.InsertParagraph($"Заказ пользователя:{_booking.Request.Client.PersonalInfo.FullName}").
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
        Paragraph para = document.InsertParagraph();
        para.AppendLine();

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

        Paragraph paragraph2 = document.InsertParagraph();
        paragraph2.Alignment = Alignment.right;
        paragraph2.AppendLine($"Дата заказа :{_booking.OrderTimestamp}").FontSize(14).Font("Times New Roman");

        Paragraph paragraph3 = document.InsertParagraph();
        paragraph3.Alignment = Alignment.right;
        paragraph3.AppendLine($"Клиент: {_booking.Request.Client.PersonalInfo.FullName} ________").FontSize(14).Bold()
            .Font("Times New Roman");
        Paragraph paragraph4 = document.InsertParagraph();
        paragraph4.Alignment = Alignment.right;
        paragraph4.AppendLine($"Сотрудник: {_booking.Worker.PersonalInfo.FullName}________").FontSize(14).Bold()
            .Font("Times New Roman");
        

        // document.AddImage(@"C:\Users\arshi\RiderProjects\PhotoStudio\PhotoStudio\Resources\3081797.png");
       
        // Picture picture = new Picture(document,
        //     ,
        //     Image.FromFile());
        // paragraph.AppendPicture((Picture))
            
        // сохраняем документ
             document.Save();
             
             Process.Start(new ProcessStartInfo
       {
           FileName = @"C:\Users\arshi\Downloads\Boba.docx",
           UseShellExecute = true
       });

    }
}