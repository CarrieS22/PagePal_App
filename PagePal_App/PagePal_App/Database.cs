using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PagePal_App
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _database;
        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Books>();
        }

        public Task<List<Books>> GetBooksAsync()
        {
            return _database.Table<Books>().ToListAsync();
        }

        public Task<int> SaveBookAsync(Books book)
        {
            return _database.InsertAsync(book);
        }

        public Task<int> DeleteAllItems<T>()
        {
            return _database.DeleteAllAsync<T>();
        }
    }
}
