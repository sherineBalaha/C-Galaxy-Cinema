namespace GalaxyCinemas
{
    partial class MainForm
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
            this.btnImport = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnExportDataForm = new System.Windows.Forms.Button();
            this.btnBookingForm = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(43, 105);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(196, 23);
            this.btnImport.TabIndex = 0;
            this.btnImport.Text = "Import Movies and Sessions ";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SketchFlow Print", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(38, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 29);
            this.label1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Lucida Calligraphy", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(30, 37);
            this.label2.MaximumSize = new System.Drawing.Size(300, 200);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(269, 44);
            this.label2.TabIndex = 5;
            this.label2.Text = "Galaxy Cinemas";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // btnExportDataForm
            // 
            this.btnExportDataForm.Location = new System.Drawing.Point(43, 195);
            this.btnExportDataForm.Name = "btnExportDataForm";
            this.btnExportDataForm.Size = new System.Drawing.Size(196, 23);
            this.btnExportDataForm.TabIndex = 7;
            this.btnExportDataForm.Text = "Export Data Form";
            this.btnExportDataForm.UseVisualStyleBackColor = true;
            this.btnExportDataForm.Click += new System.EventHandler(this.btnExportDataForm_Click);
            // 
            // btnBookingForm
            // 
            this.btnBookingForm.Location = new System.Drawing.Point(43, 148);
            this.btnBookingForm.Name = "btnBookingForm";
            this.btnBookingForm.Size = new System.Drawing.Size(196, 23);
            this.btnBookingForm.TabIndex = 8;
            this.btnBookingForm.Text = "Booking Form";
            this.btnBookingForm.UseVisualStyleBackColor = true;
            this.btnBookingForm.Click += new System.EventHandler(this.btnBookingForm_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(118, 309);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 458);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnBookingForm);
            this.Controls.Add(this.btnExportDataForm);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnImport);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnExportDataForm;
        private System.Windows.Forms.Button btnBookingForm;
        private System.Windows.Forms.Button btnExit;
    }
}