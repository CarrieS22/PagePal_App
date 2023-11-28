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

        //Get books
        public Task<List<Books>> GetBooksAsync()
        {
            return _database.Table<Books>().ToListAsync();
        }

        //Get Authors
        public Task<List<Books>> GetDistinctAuthorsAsync()
        {
            return _database.Table<Books>().ToListAsync(); 
        }

        //Get books based on filters
        public Task<List<Books>> GetFilteredBooks(string genre, string author)
        {
            // Construct the query based on the provided filters
            var query = _database.Table<Books>();

            if (!string.IsNullOrEmpty(genre))
            {
                query = query.Where(b => b.Genre == genre);
            }

            if (!string.IsNullOrEmpty(author))
            {
                query = query.Where(b => $"{b.AuthorFirstName} {b.AuthorLastName}" == author);
            }

            // Execute the query and return the result
            return query.ToListAsync();
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
