using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyCinemas
{
    public class CompletedEventArgs : EventArgs
    {
        public ImportResult Result { get; set; }

        public CompletedEventArgs(ImportResult result)
        {
            Result = result;
        }
    }
}
