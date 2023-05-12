using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using PhotoStudio.Models.DataBase;

namespace PhotoStudio.Views;

public partial class PhotoClientView : UserControl
{
    private readonly OpenFileDialog _openFileDialog;
    private ClientPhotos _clientPhotos;
    private Client _client;
    public PhotoClientView(Client client)
    {
        InitializeComponent();
        _openFileDialog = new OpenFileDialog();
        _clientPhotos = new ClientPhotos();
        
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
        
    }
}