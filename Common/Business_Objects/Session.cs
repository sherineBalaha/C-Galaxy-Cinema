using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Business_Objects
{
    public class Session
    {
        //public properties with setter and getters
        public int SessionID { get; set; }
        public int MovieID { get; set; }
        public DateTime SessionDate { get; set; }
        public byte CinemaNumber { get; set; }

        //user-friendly String format
        public string shortFormat {
            get
            {
                return String.Format("{0 :HH :mm } - Cinema {1}", this.SessionDate, this.CinemaNumber);
            }
        } 

    }
}
