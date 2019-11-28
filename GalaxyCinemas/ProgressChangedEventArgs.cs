using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyCinemas
{
    public class ProgressChangedEventArgs : EventArgs
    {
        public float Progress { get; set; }

        public ProgressChangedEventArgs(float progress)
        {
            Progress = progress;
        }
    }
}
