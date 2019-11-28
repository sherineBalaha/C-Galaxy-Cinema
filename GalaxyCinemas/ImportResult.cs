using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalaxyCinemas
{
    public class ImportResult
    {
        //private properties with setters and getters
        public int TotalRows { get; set; }
        public int ImportedRows { get; set; }
        public int FailedRows { get; set; }

        public List<String> ErrorMessages 
        {
            get { return ErrorMessages; }
        }

        //public constructor resetting values
        public ImportResult()
        {
            this.TotalRows = 0;
            this.ImportedRows = 0;
            this.FailedRows = 0;
            this.ErrorMessages.Clear();

        }
        
        

       
    }
}
