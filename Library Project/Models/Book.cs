using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_Project.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string BookDescription { get; set; }
        public byte[] BookImage { get; set; }
       
    }
}