using System;
namespace GalaxyCinemas
{
    public delegate void CompletedEventHandler(object sender, CompletedEventArgs args);
    public delegate void ProgressChangedEventHandler(object sender, ProgressChangedEventArgs args);

    /// <summary>
    /// Common functionality for importers.
    /// </summary>
    public abstract class BaseImporter
    {

        protected string fileName = null;

        public event CompletedEventHandler Completed;
        public event ProgressChangedEventHandler ProgressChanged;

        protected void RaiseCompleted(ImportResult result)
        {
            if (Completed != null)
                Completed(this, new CompletedEventArgs(result));
        }

        protected void RaiseProgressChanged()
        {
            if (ProgressChanged != null)
                ProgressChanged(this, new ProgressChangedEventArgs(Progress));
        }

        /// <summary>
        /// Set to stop the import.
        /// </summary>
        public bool Stop { protected get; set; }
        /// <summary>
        /// Progress from 0 to 1.
        /// </summary>
        protected float Progress { get; set; }

        /// <summary>
        /// Takes object to match signature of WaitCallback.
        /// </summary>
        /// <param name="o"></param>
        public abstract void Import(object o);

        

        public BaseImporter(string filename)
        {
            this.fileName = filename;
        }
    }
}
