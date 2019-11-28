using System;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace GalaxyCinemas
{
    public partial class ImportDataForm : Form
    {
        private MovieImporter movieImporter = null;
        private SessionImporter sessionImporter = null;

        public ImportDataForm()
        {
            InitializeComponent();

            this.FormClosing += ImportDataForm_FormClosing;
        }

        #region Movie importer

        /// <summary>
        /// Open a dialog to pick a file, check it can be opened.
        /// </summary>
        private void btnSelectMovieFile_Click(object sender, EventArgs e)
        {
           //filter to show files .cvs format only
           opnFileDialog.Filter = "*.csv";

           // Opens a dialog to pick a file to import.
           DialogResult result =  opnFileDialog.ShowDialog();
           if (result != System.Windows.Forms.DialogResult.OK)
               return;
           
            // Checks that the file is valid.
            try
            {
                TextReader tr = File.OpenText(opnFileDialog.FileName);
                txtMovieFileName.Text = opnFileDialog.FileName;
                tr.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error opening file");
            }
        }

        /// <summary>
        /// Starts the movie import. Sets up UI appropriately and starts a new thread.
        /// </summary>
        private void btnMovieImportStart_Click(object sender, EventArgs e)
        {
            // Validate the movie filename.
            blockSubmitIfValidationFails = true;
            try
            {
                txtMovieFileName.Focus();
                if (!Validate())
                    return;
            }
            finally
            {
                blockSubmitIfValidationFails = false;
            }

            // Do UI changes, e.g. hide Start button, show Stop button, start progress bar.
            btnMovieImportStart.Visible = false;
            btnMovieImportStop.Visible = true;
            progressBar.Value = 0;
            progressBar.Visible = true;

            // Start importer in another thread.
            movieImporter = new MovieImporter(txtMovieFileName.Text);
            movieImporter.Completed += new CompletedEventHandler(MovieImportFinished);
            movieImporter.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            ThreadPool.QueueUserWorkItem(new WaitCallback(movieImporter.Import));
        }

        /// <summary>
        /// Stops the movie import.
        /// </summary>
        private void btnMovieImportStop_Click(object sender, EventArgs e)
        {
            // Sets a property on the movie importer, which will stop when next convenient.
            movieImporter.Stop = true;
        }

        /// <summary>
        /// Event handler that gets called when the movie import is finished.
        /// </summary>
        private void MovieImportFinished(object sender, CompletedEventArgs args)
        {
            ImportResult result = args.Result;
            movieImporter = null;
            Invoke((Action)(() =>
            {
                btnMovieImportStart.Visible = true;
                btnMovieImportStop.Visible = false;
                progressBar.Visible = false;

                ImportResultsPopup popup = new ImportResultsPopup(result);
                popup.ShowDialog();
            }));
        }

        #endregion

        #region Session importer
        /// <summary>
        /// Open a dialog to pick a file, check it can be opened.
        /// </summary>
        //btnSelectSessionFile
        private void btnSelectSessionFile_Click(object sender, EventArgs e)
        {
            //filter to show files .cvs format only
            opnFileDialog.Filter = "*.csv";

            // Opens a dialog to pick a file to import.
            DialogResult result = opnFileDialog.ShowDialog();
            if (result != System.Windows.Forms.DialogResult.OK)
                return;

            // Checks that the file is valid.
            try
            {
                TextReader tr = File.OpenText(opnFileDialog.FileName);
                txtSessionFileName.Text = opnFileDialog.FileName;
                tr.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error opening file");
            }

        }

        /// <summary>
        /// Starts the session import. Sets up UI appropriately and starts a new thread.
        /// </summary>
        private void btnSessionImportStart_Click(object sender, EventArgs e)
        {
            // Validate the movie filename.
            blockSubmitIfValidationFails = true;
            try
            {
                txtSessionFileName.Focus();
                if (!Validate())
                    return;
            }
            finally
            {
                blockSubmitIfValidationFails = false;
            }

            // Do UI changes, e.g. hide Start button, show Stop button, start progress bar.
            btnSessionImportStart.Visible = false;
            btnSessionImportStop.Visible = true;
            progressBar.Value = 0;
            progressBar.Visible = true;

            // Start importer in another thread.
            sessionImporter = new SessionImporter(txtSessionFileName.Text);
            //here
            sessionImporter.Completed += new CompletedEventHandler(SessionImportFinished);
            sessionImporter.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            ThreadPool.QueueUserWorkItem(new WaitCallback(sessionImporter.Import));

        }

        /// <summary>
        /// Stops the movie import.
        /// </summary>
        private void btnSessionImportStop_Click(object sender, EventArgs e)
        {
            // Sets a property on the Session importer, which will stop when next convenient.
            sessionImporter.Stop = true;
        }

        /// <summary>
        /// Event handler that gets called when the session import is finished.
        /// </summary>
        private void SessionImportFinished(object sender, CompletedEventArgs args)
        {
            ImportResult result = args.Result;
            sessionImporter = null;
            Invoke((Action)(() =>
            {
                btnSessionImportStart.Visible = true;
                btnSessionImportStop.Visible = false;
                progressBar.Visible = false;

                ImportResultsPopup popup = new ImportResultsPopup(result);
                popup.ShowDialog();
            }));
        }

        #endregion


        /// <summary>
        /// Updates the progress bar.
        /// </summary>
        private void ProgressChanged(object sender, ProgressChangedEventArgs args)
        {
            float value = args.Progress;
            Invoke(new Action(() => { progressBar.Value = (int)(value * 100); }));
        }

        #region Form validation

        // We set this to true to apply stricter validation, e.g. prevent continuing form submission.
        private bool blockSubmitIfValidationFails = false;

        private void txtMovieFileName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Check if file path is valid.
            bool pathValid = true;
            try
            {
                FileInfo fi = new FileInfo(txtMovieFileName.Text);
                pathValid = fi.Directory.Exists;
            }
            catch (Exception)
            {
                pathValid = false;
            }

            if (!pathValid)
            {
                errorProvider.SetError(txtMovieFileName, "Please choose a valid movie file to import");
                e.Cancel = blockSubmitIfValidationFails; // Don't allow moving to the next field if we're submitting the form.
            }
            else errorProvider.SetError(txtMovieFileName, ""); // Clear error if all fine.
        }

        private void txtSessionFileName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Check if file path is valid.
            bool pathValid = true;
            try
            {
                FileInfo fi = new FileInfo(txtSessionFileName.Text);
                pathValid = fi.Directory.Exists;
            }
            catch (Exception)
            {
                pathValid = false;
            }

            if (!pathValid)
            {
                errorProvider.SetError(txtSessionFileName, "Please choose a valid session file to import");
                e.Cancel = blockSubmitIfValidationFails; // Don't allow moving to the next field if we're submitting the form.
            }
            else errorProvider.SetError(txtSessionFileName, ""); // Clear error if all fine.
        }

        private void ImportDataForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Don't allow validation to prevent closing.
            e.Cancel = false;
        }

        #endregion

        

        private void txtMovieFileName_TextChanged(object sender, EventArgs e)
        {

        }

        private void ImportDataForm_Load(object sender, EventArgs e)
        {

        }

    }
}
