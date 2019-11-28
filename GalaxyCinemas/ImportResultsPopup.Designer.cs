namespace GalaxyCinemas
{
    partial class ImportResultsPopup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblTotalLabel = new System.Windows.Forms.Label();
            this.lblImportedLabel = new System.Windows.Forms.Label();
            this.lblFailedLabel = new System.Windows.Forms.Label();
            this.lblTotalValue = new System.Windows.Forms.Label();
            this.lblImportedValue = new System.Windows.Forms.Label();
            this.lblFailedValue = new System.Windows.Forms.Label();
            this.pnlErrors = new System.Windows.Forms.Panel();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlErrors.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(112, 17);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Import Results";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTotalLabel
            // 
            this.lblTotalLabel.AutoSize = true;
            this.lblTotalLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTotalLabel.Location = new System.Drawing.Point(13, 41);
            this.lblTotalLabel.Name = "lblTotalLabel";
            this.lblTotalLabel.Size = new System.Drawing.Size(75, 13);
            this.lblTotalLabel.TabIndex = 1;
            this.lblTotalLabel.Text = "Total Rows:";
            // 
            // lblImportedLabel
            // 
            this.lblImportedLabel.AutoSize = true;
            this.lblImportedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblImportedLabel.Location = new System.Drawing.Point(12, 63);
            this.lblImportedLabel.Name = "lblImportedLabel";
            this.lblImportedLabel.Size = new System.Drawing.Size(95, 13);
            this.lblImportedLabel.TabIndex = 2;
            this.lblImportedLabel.Text = "Imported Rows:";
            // 
            // lblFailedLabel
            // 
            this.lblFailedLabel.AutoSize = true;
            this.lblFailedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFailedLabel.Location = new System.Drawing.Point(12, 86);
            this.lblFailedLabel.Name = "lblFailedLabel";
            this.lblFailedLabel.Size = new System.Drawing.Size(80, 13);
            this.lblFailedLabel.TabIndex = 3;
            this.lblFailedLabel.Text = "Failed Rows:";
            // 
            // lblTotalValue
            // 
            this.lblTotalValue.AutoSize = true;
            this.lblTotalValue.Location = new System.Drawing.Point(142, 41);
            this.lblTotalValue.Name = "lblTotalValue";
            this.lblTotalValue.Size = new System.Drawing.Size(0, 13);
            this.lblTotalValue.TabIndex = 4;
            // 
            // lblImportedValue
            // 
            this.lblImportedValue.AutoSize = true;
            this.lblImportedValue.Location = new System.Drawing.Point(142, 63);
            this.lblImportedValue.Name = "lblImportedValue";
            this.lblImportedValue.Size = new System.Drawing.Size(0, 13);
            this.lblImportedValue.TabIndex = 5;
            // 
            // lblFailedValue
            // 
            this.lblFailedValue.AutoSize = true;
            this.lblFailedValue.Location = new System.Drawing.Point(142, 86);
            this.lblFailedValue.Name = "lblFailedValue";
            this.lblFailedValue.Size = new System.Drawing.Size(0, 13);
            this.lblFailedValue.TabIndex = 6;
            // 
            // pnlErrors
            // 
            this.pnlErrors.AutoScroll = true;
            this.pnlErrors.Controls.Add(this.flowLayoutPanel);
            this.pnlErrors.Location = new System.Drawing.Point(16, 114);
            this.pnlErrors.Name = "pnlErrors";
            this.pnlErrors.Size = new System.Drawing.Size(256, 136);
            this.pnlErrors.TabIndex = 7;
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.AutoScroll = true;
            this.flowLayoutPanel.AutoSize = true;
            this.flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(256, 136);
            this.flowLayoutPanel.TabIndex = 0;
            this.flowLayoutPanel.WrapContents = false;
            // 
            // ImportResultsPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.pnlErrors);
            this.Controls.Add(this.lblFailedValue);
            this.Controls.Add(this.lblImportedValue);
            this.Controls.Add(this.lblTotalValue);
            this.Controls.Add(this.lblFailedLabel);
            this.Controls.Add(this.lblImportedLabel);
            this.Controls.Add(this.lblTotalLabel);
            this.Controls.Add(this.lblTitle);
            this.Name = "ImportResultsPopup";
            this.Text = "Import Results";
            this.pnlErrors.ResumeLayout(false);
            this.pnlErrors.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblTotalLabel;
        private System.Windows.Forms.Label lblImportedLabel;
        private System.Windows.Forms.Label lblFailedLabel;
        private System.Windows.Forms.Label lblTotalValue;
        private System.Windows.Forms.Label lblImportedValue;
        private System.Windows.Forms.Label lblFailedValue;
        private System.Windows.Forms.Panel pnlErrors;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
    }
}