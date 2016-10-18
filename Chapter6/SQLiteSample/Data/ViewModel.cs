using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteSample.Data
{
    public class ViewModel : INotifyPropertyChanged
    {
        IDataRepository _data;

        public ViewModel(IDataRepository data)
        {
            _data = data;
        }

        private Customer _selectedItem;

        public Customer SelectedItem
        {
            get
            {
                return this._selectedItem;
            }
            set
            {
                if (value != _selectedItem)
                {
                    _selectedItem = value;
                    RaisePropertyChanged();
                }
            }
        }

        private ObservableCollection<Customer> _customers;

        public ObservableCollection<Customer> Customers
        {
            get
            {
                return _customers;
            }
            set
            {
                _customers = value;
                RaisePropertyChanged();
            }
        }

        public async void Initialize()
        {
            Customers = await _data.Load(); 
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName]
                                          string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }

        internal void AddCustomer(Customer cust)
        {
            _data.Add(cust);
            RaisePropertyChanged();
        }

        internal void DeleteCustomer(Customer cust)
        {
            _data.Remove(cust);
            RaisePropertyChanged();
        }
        
    }
}
