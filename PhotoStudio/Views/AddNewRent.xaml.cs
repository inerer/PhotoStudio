using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using PhotoStudio.DataBase.Repositories;
using PhotoStudio.Models.DataBase.SupplyRequestModels;
using PhotoStudio.Services;

namespace PhotoStudio.Views;

public partial class AddNewRent : UserControl
{
    private readonly Rent _rent;
    private readonly RentService _rentService;
    private readonly OpenFileDialog _openFileDialog;
    private readonly HallService _hallService;
    private readonly Hall _hall;
    
    public AddNewRent()
    {
        InitializeComponent();
        _rent = new Rent();
        _rentService = new RentService();
        _openFileDialog = new OpenFileDialog();
        _hallService = new HallService();
        _hall = new Hall();
        this.DataContext = _hall;
    }


    private void ImageBorder_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        try
        {
            _openFileDialog.FileName = "";
            _openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                                     "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                                     "Portable Network Graphic (*.png)|*.png";
            bool? result = _openFileDialog.ShowDialog();
            if (result == true)
                Image.Source = new BitmapImage(new Uri(_openFileDialog.FileName));
        }
        catch
        {
            MessageBox.Show("Произошла непредвиденная ошибка");
        }
    }

    private void ImageBorder_OnDrop(object sender, DragEventArgs e)
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
                    Image.Source = new BitmapImage(new Uri(file));
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

    private void AddButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            _hall.Photo = _openFileDialog.FileName;
            _rent.PriceHour = Convert.ToDecimal(PriceNumberBox.Text);
            _rent.Hall = _hallService.AddHall(_hall);
            MessageBox.Show("Успешно");
        }
        catch (Exception exception)
        {
            MessageBox.Show("Ошибка");
        }
        
    }
}