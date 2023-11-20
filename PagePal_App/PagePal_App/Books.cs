using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PagePal_App
{
    public class Books
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string BookTitle { get; set; }
        public string AuthorLastName { get; set; }
        public string AuthorFirstName { get; set; }
        public DateTime yearEntry { get; set; }
        public string Genre { get; set; }
        public string Publication { get; set; }
    }
}
