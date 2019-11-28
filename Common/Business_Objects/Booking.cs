using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Business_Objects
{
    public class Booking
    {
        //public properties with setter and getters
        public int BookingNumber { get; set; }
        public int SessionID { get; set; }

        public DateTime SessionDate { get; set; }
        public int Quantity { get; set; }
        public string Special { get; set; }

        public decimal Discount { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal FinalPrice  { get; set; }



    }
}
