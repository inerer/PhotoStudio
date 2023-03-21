using System.Windows;
using System.Windows.Controls;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Services;

namespace PhotoStudio.Views;

public partial class ClientNameView : UserControl
{
    private readonly PersonalInfo _personalInfo;
    private readonly PersonalInfoService _personalInfoService;
    private readonly Client _client;
    public ClientNameView()
    {
        InitializeComponent();
        _personalInfo = new();
        _client = new();
        this.DataContext = _personalInfo;
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void SubmitButton_OnClick(object sender, RoutedEventArgs e)
    {
      //  _client.PersonalInfo =
      _personalInfoService.AddPersonalInfo(_personalInfo);
      
      
    }
}