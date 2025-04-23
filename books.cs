using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class books
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }

        public string Genre { get; set; }

        public int Year_published { get; set; }

        public bool Available { get; set; }


        public string AuthorFullName => $"{AuthorFirstName} {AuthorLastName}";
        
        /*
      public DateTime Expiration_date { get; set; }*/
    }
}
