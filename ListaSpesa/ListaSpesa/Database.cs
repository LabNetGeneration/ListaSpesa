using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace ListaSpesa
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;
        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Prodotto>().Wait();
        }
        public Task<List<Prodotto>> GetProdottoAsync()
        {
            return _database.Table<Prodotto>().ToListAsync();
        }
        public Task<int> SaveProdottoAsync(Prodotto prodotto)
        {
            return _database.InsertAsync(prodotto);
        }
        public Task<int> DeleteProdottoAsync(Prodotto prodotto)
        {
            return _database.DeleteAsync(prodotto);
        }
    }
}

