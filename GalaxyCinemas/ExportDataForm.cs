using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

//added statement to reference Business_Objects Library,and serialization library and others
using Common.Business_Objects;
using Common;
using System.Xml.Serialization;
using System.Xml;

namespace GalaxyCinemas
{
    public partial class ExportDataForm : Form
    {
        //string to save fleName
        public static string fileName = "";

        public ExportDataForm()
        {

            InitializeComponent();
           
            this.FormClosing += ExportDataForm_FormClosing;

            //registering the event handler for browsing and saving files button and back button
            this.btnSelectExportBooking.Click += btnSelectExportBooking_Click;
            this.btn_Back .Click += btn_Back_Click;
        }

        /// <summary>
        /// Allows user to browse to a save location for the XML file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectExportBooking_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Export Data";
            DialogResult dialogueResult =  saveFileDialog.ShowDialog();
            fileName = saveFileDialog.FileName;

            if (fileName != "")
            {
                
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = saveFileDialog.FileName;
                    txtFileBooking.Text = selectedPath;
                }  

            }

            // Set focus on this field. Moving focus will force validation of the value.
            txtFileBooking.Focus(); 
        }

        

        /// <summary>
        
        /// </summary>
        /// <param name="list"></param>
        
            // Serialize
            public void Serialise (List<Booking> list,string fileName )
            {
                
                // Serialize bookings to XML file
                XmlSerializer serializer = new XmlSerializer(typeof(List<Booking>));
                string xml;
                using (StringWriter writer = new StringWriter())
                {
                    serializer.Serialize(writer, list);
                    xml = writer.ToString();
                    
                    //Export bookings to XML file.
                    XmlDocument xmlBookingDoc = new XmlDocument();
                    xmlBookingDoc.LoadXml(xml);
                    xmlBookingDoc.Save(fileName);

                   
                }


            }
 
     
        /// <summary>
        /// Closes the form and goes back to main menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        

        #region Form validation

        /// <summary>
        /// Check all form fields are valid. This works even if they haven't clicked into every field.
        /// </summary>
        /// <returns></returns>
        private bool IsFormValid()
        {
            foreach (Control control in Controls)
            {
                // Set focus on control
                control.Focus();
                // Validate causes the control's Validating event to be fired,
                // if CausesValidation is True
                if (!Validate())
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Validate the filename.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFileBooking_Validating(object sender, CancelEventArgs e)
        {
            // Check if file path is valid.
            bool pathValid = true;
            try
            {
                FileInfo fi = new FileInfo(txtFileBooking.Text);
                pathValid = fi.Directory.Exists;
            }
            catch (Exception)
            {
                pathValid = false;
            }

            if (!pathValid)
            {
                errorProvider.SetError(txtFileBooking, "Please choose a valid path to export to");
                e.Cancel = true; // Don't allow moving to the next field.
            }
            else errorProvider.SetError(txtFileBooking, ""); // Clear error if all fine.
        }

        /// <summary>
        /// Validate the to date.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpTo_Validating(object sender, CancelEventArgs e)
        {
            if (dtpTo.Value.Date < dtpFrom.Value.Date)
            {
                errorProvider.SetError(dtpTo, "The 'to' date must be greater than or equal to the 'from' date");
                e.Cancel = true; // Don't allow moving to the next field.
            }
            else errorProvider.SetError(dtpTo, ""); // Clear error if all fine.
        }

        private void ExportDataForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Don't allow validation to prevent closing.
            e.Cancel = false;
        }

        #endregion

        private void ExportDataForm_Load(object sender, EventArgs e)
        {

        }

        private void btn_Back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExportBookings_Click(object sender, EventArgs e)
        {
            //checking validations
            if (this.IsFormValid())
            {
                try
                {
                DateTime dtpFromDate = Convert.ToDateTime(dtpFrom.Text);
                DateTime dtpToDate = Convert.ToDateTime(dtpTo.Text);
                List<Booking> bookings = new List<Booking>();
                bookings = GalaxyCinemas.DataLayer.GetBookingsInDateRange(dtpFromDate, dtpToDate);

                //serialise and export xml doc
                Serialise(bookings, fileName);
                int numberOfBookingsExported = bookings.Count;

                //display
                MessageBox.Show("Number of exported bookings successfully is : " + numberOfBookingsExported);

                }

                catch(Exception e)
                {
                    errorProvider.SetError(null, "An error has occurred while booking ");
                }

            }
            else
            {
                
                return;
            }




        }

       
    }
}
