using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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

        [Table("authors")]
        public class Authors
        {
            [PrimaryKey, AutoIncrement]
            public int AuthorID { get; set; }
            public string ALastName { get; set; }
            public string AFirstName { get; set; }
        }

        [Table("booktable")]
        public class BookTable
        {
            [PrimaryKey, AutoIncrement]
            public int BookID { get; set; }
            public string Genre { set; get; }
            public string BPublication { get; set; }
        }

        [Table("toread")]
        public class ToRead
        {
            [PrimaryKey, AutoIncrement]
            public int ToReadID { get; set; }
        }

        [Table("users")]
        public class Users
        {
            [PrimaryKey, AutoIncrement]
            public int UserID { get; set; }
            public string username { get; set; }
            public string email { get; set; }
            public string UFirstName { get; set; }
            public string ULastName { get; set; }
        }
    }
}
