using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using SQLiteSample.Data;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SQLiteSample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private IDataRepository data = new SQLiteRepository();
        private ViewModel _vm;

        public MainPage()
        {
            this.InitializeComponent();
            _vm = new ViewModel(data);
            _vm.Initialize();
            DataContext = _vm;
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_vm.SelectedItem != null)
            {
                _vm.DeleteCustomer(_vm.SelectedItem);
            }
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            Customer cust = new Customer
            {
                Email = Email.Text,
                FirstName = FirstName.Text,
                LastName = LastName.Text,
                Title = Title.Text
            };
            _vm.AddCustomer(cust);
        }
    }
}