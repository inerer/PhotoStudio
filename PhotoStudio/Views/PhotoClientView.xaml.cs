using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using PhotoStudio.DataBase.Repositories;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Models.DataBase.SupplyRequestModels;
using PhotoStudio.Services;

namespace PhotoStudio.Views;

public partial class PhotoClientView : UserControl
{
    private readonly OpenFileDialog _openFileDialog;
    private ClientPhotos _clientPhotos;
    private ClientPhotosRepository _clientPhotosRepository;
    private Request _request;
    private Client _client;
    private RequestService _requestService;
    private ClientPhotosService _clientPhotosService;
    public PhotoClientView(Client client)
    {
        InitializeComponent();
        _openFileDialog = new OpenFileDialog();
        _clientPhotos = new ClientPhotos();
        _clientPhotosRepository = new ClientPhotosRepository();
        _client = client;
        _request = new Request();
        _requestService = new RequestService();
        _clientPhotosService = new ClientPhotosService();

    }

    private void ClientPhotoBorder_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        try
        {
            _openFileDialog.FileName = "";
            _openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                                    "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                                    "Portable Network Graphic (*.png)|*.png";
            bool? result = _openFileDialog.ShowDialog();
            if (result == true)
                PhotoImage.Source = new BitmapImage(new Uri(_openFileDialog.FileName));
        }
        catch
        {
            MessageBox.Show("Произошла непредвиденная ошибка");
        }
    }

    private void ClientPhotoBorder_OnDrop(object sender, DragEventArgs e)
    {
        try
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 1)
                MessageBox.Show("Нужно выбрать 1 файл!");
            else
            {
                string file = files[0];
                if (file.EndsWith(".png") || file.EndsWith(".jpg") || file.EndsWith(".jpeg"))
                {
                    Image image = new Image();
                    image.Width = 150;
                    image.Height = 150;
                    image.Source = new BitmapImage(new Uri(file));
                    PhotoImage.Source = new BitmapImage(new Uri(file));
                    _openFileDialog.FileName = files[0];
                }
                else
                {
                    MessageBox.Show("Только картинки");
                }
            }
        }
        catch
        {
            MessageBox.Show("Ошибка");

        }
    }

    private void AddPhotoButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            _clientPhotos.Request = _requestService.GetRequestByClientId(_client);
            _clientPhotos.Photo = _openFileDialog.FileName;
            _clientPhotosService.AddClientPhotos(_clientPhotos);
            MessageBox.Show("Фото добавлено");
        }
        catch (Exception exception)
        {
            MessageBox.Show("Сначала сделайте заказ!");
        }
        

    }
}