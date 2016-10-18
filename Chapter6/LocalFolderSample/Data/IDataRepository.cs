using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalFolderSample.Data
{
    public interface IDataRepository<T>
    {
        Task Add(T customer);

        Task<ObservableCollection<T>> Load();

        Task Remove(T customer);

        Task Update(T customer);
    }
}