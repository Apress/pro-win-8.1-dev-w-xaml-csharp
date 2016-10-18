using System;
using System.Linq;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AppSettings
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
public sealed partial class MainPage : Page
{
    private ApplicationDataContainer settings = ApplicationData.Current.LocalSettings;
    string settingName = "UserSetting";
        
    public MainPage()
    {
        this.InitializeComponent();
    }

    private void SaveSettings_Click(object sender, RoutedEventArgs e)
    {
        string userValue = txtSettings.Text;
        settings.Values[settingName] = userValue;
        txtSettings.Text = String.Empty;
    }

    private void RetrieveSettings_Click(object sender, RoutedEventArgs e)
    {
        object val = settings.Values[settingName];
        if (val != null)
        {
            txtSettingOutput.Text = val.ToString();
        }
    }

    private void DeleteSettings_Click(object sender, RoutedEventArgs e)
    {
        settings.Values.Remove(settingName);
        txtSettingOutput.Text = String.Empty;
    }
}
}