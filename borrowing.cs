using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class borrowing
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ReaderFN { get; set; }
        public string ReaderLN { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime Expiration_date { get; set; }

        public string FullReaderName => $"{ReaderFN} {ReaderLN}";

        public string ReturnDateShort =>
            ReturnDate == DateTime.MinValue ? "-" : ReturnDate.ToShortDateString();

        public string ExpirationDateShort => Expiration_date.ToShortDateString();
    }

}















