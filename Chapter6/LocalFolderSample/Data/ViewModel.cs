using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace LocalFolderSample.Data
{
	public class ViewModel : INotifyPropertyChanged
	{
		IDataRepository<Customer> _repo;

		public ViewModel(IDataRepository<Customer> repo)
		{
			_repo = repo;
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
			Customers = await _repo.Load(); 
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void RaisePropertyChanged([CallerMemberName] string fieldName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(fieldName));
			}
		}

		internal void AddCustomer(Customer cust)
		{
			_repo.Add(cust);
		}

		internal void DeleteCustomer(Customer cust)
		{
			_repo.Remove(cust);
		}
	}
}