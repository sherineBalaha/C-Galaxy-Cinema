using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GalaxyCinemas
{
    public partial class ImportResultsPopup : Form
    {
        public ImportResultsPopup(ImportResult result)
        {
            InitializeComponent();

            lblTotalValue.Text = result.TotalRows.ToString();
            lblImportedValue.Text = result.ImportedRows.ToString();
            lblFailedValue.Text = result.FailedRows.ToString();

            foreach (string err in result.ErrorMessages)
            {
                Label lblErr = new Label() { Text = err, AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.Right };
                flowLayoutPanel.Controls.Add(lblErr);
            }
        }
    }
}
