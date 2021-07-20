using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biblioteca.Models
{
    public class Models
    {
        public Book book { get; set; }
        public User user { get; set; }
        public Comment comment { get; set; }
        public Valoration valoration { get; set; }
    }
}