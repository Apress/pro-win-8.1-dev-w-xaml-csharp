using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteSample.Data;

namespace SQLiteSample.Data
{
    public interface IDataRepository
    {
        Task Add(Customer customer);

        Task<ObservableCollection<Customer>> Load();

        Task Remove(Customer customer);

        Task Update(Customer customer);
    }
}
