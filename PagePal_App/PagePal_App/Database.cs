using MongoDB.Bson.Serialization;
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
            _database.CreateTableAsync<BookTables.Books>();

        }

        //Get books
        public Task<List<BookTables.Books>> GetBooksAsync()
        {
            return _database.Table<BookTables.Books>().ToListAsync();
        }

        //Get Authors
        public Task<List<BookTables.Books>> GetDistinctAuthorsAsync()
        {
            return _database.Table<BookTables.Books>().ToListAsync(); 
        }

        //Get books based on filters
        public async Task<List<BookTables.Books>> GetBooksBasedOnFiltersAsync(string genre, string[] authorNames)
        {
            // Create a base query to retrieve books
            var query = _database.Table<BookTables.Books>();

            // Filter by genre if specified
            if (!string.IsNullOrEmpty(genre))
            {
                query = query.Where(b => b.Genre == genre);
            }

            // Filter by author if specified
            if (authorNames != null && authorNames.Length > 0)
            {
                // Assuming authorNames[0] is the first name and authorNames[1] is the last name
                query = query.Where(b => b.AuthorFirstName == authorNames[0] && b.AuthorLastName == authorNames[1]);
            }

            // Execute the query and return the results
            return await query.ToListAsync();
        }

        public Task<int> SaveBookAsync(BookTables.Books book)
        {
            return _database.InsertAsync(book);
        }

        public Task<int> DeleteAllItems<T>()
        {
            return _database.DeleteAllAsync<T>();
        }

        public Task<int> UpdateBook(BookTables.Books book)
        {
            return _database.UpdateAsync(book);
        }

        public Task<int> DeleteBook (BookTables.Books book)
        {
            return _database.DeleteAsync(book);
        }
    }
}
