using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using Windows.Storage;

namespace SQLiteSample.Data
{
    public class SQLiteRepository : IDataRepository
    {
        private static readonly string _dbPath =
            Path.Combine(ApplicationData.Current.LocalFolder.Path, "app.SQLite");
        ObservableCollection<Customer> _customers;

        public SQLiteRepository()
        {
            Initialize();
        }

        public async void Initialize()
        {
            using (var db = new SQLite.SQLiteConnection(_dbPath))
            {
                db.CreateTable<Customer>();

                //Note: This is a simplistic initialization scenario
                if (db.ExecuteScalar<int>("select count(1) from Customer") == 0)
                {
                    db.RunInTransaction(() =>
                    {
                        db.Insert(new Customer() { FirstName = "Phil", LastName = "Japikse" });
                        db.Insert(new Customer() { FirstName = "Jon", LastName = "Galloway" });
                        db.Insert(new Customer() { FirstName = "Jesse", LastName = "Liberty" });
                    });
                }
                else
                {
                    await Load();
                }
            }
        }
        
        public Task Add(Customer customer)
        {
            _customers.Add(customer);
            var connection = new SQLiteAsyncConnection(_dbPath);
            return connection.InsertAsync(customer);
        }

        public Task Remove(Customer customer)
        {
            _customers.Remove(customer);
            var connection = new SQLiteAsyncConnection(_dbPath);
            return connection.DeleteAsync(customer);
        }

        public async Task<ObservableCollection<Customer>> Load()
        {
            var list = new ObservableCollection<Customer>();
            var connection = new SQLiteAsyncConnection(_dbPath);

            _customers = new ObservableCollection<Customer>(await connection.QueryAsync<Customer>("select * from Customer"));
            return _customers;
        }

        public Task Update(Customer customer)
        {
            var oldCustomer = _customers.FirstOrDefault(c => c.Id == customer.Id);
            if (oldCustomer == null)
            {
                throw new System.ArgumentException("Customer not found.");
            }
            _customers.Remove(oldCustomer);
            _customers.Add(customer);
            var connection = new SQLiteAsyncConnection(_dbPath);
            return connection.UpdateAsync(customer);
        }
    }
}