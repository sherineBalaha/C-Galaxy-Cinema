using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

//added statement to reference Business_Objects Library,and serialization library and others
using Common.Business_Objects;
using Common;
using System.Xml.Serialization;
using System.Xml;


namespace GalaxyCinemas
{
    public partial class MainForm : Form
    {
        public List<ISpecialPlugin> specialPlugins = new List<ISpecialPlugin>();

        public MainForm()
        {
            InitializeComponent();
            

            try
            {
                DirectoryInfo dir = new DirectoryInfo(Application.StartupPath);

                foreach (FileInfo file in dir.GetFiles("Plugin*.dll"))
                {
                    string name = Path.GetFileNameWithoutExtension(file.Name);
                    
                    //Inspect on assebly
                    Assembly pluginAssembly = Assembly.Load("name");

                    var plugins = from type in pluginAssembly.GetTypes()
                                  where typeof(ISpecialPlugin).IsAssignableFrom(type) && !type.IsInterface
                                  select type;

                    foreach (Type pluginType in plugins)
                    {
                        ISpecialPlugin plugin = Activator.CreateInstance(pluginType) as ISpecialPlugin;
                        specialPlugins.Add(plugin);
                    }

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurred while loading special pricing plugins!");
            }
        }

        private void ChildFormClosed(object sender, FormClosedEventArgs e)
        {
            // To ensure the main form has focus after a child form is closed.
            this.Focus();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            ImportDataForm idf = new ImportDataForm();
            idf.FormClosed += ChildFormClosed;
            idf.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
            
        }

        private void btnImportDataForm_Click(object sender, EventArgs e)
        {
            ImportDataForm idf = new ImportDataForm();
            idf.FormClosed += ChildFormClosed;
            idf.Show();
        }

        private void btnBookingForm_Click(object sender, EventArgs e)
        {
            BookingForm bf = new BookingForm(specialPlugins);
            bf.FormClosed += ChildFormClosed;
            bf.Show();

        }

        private void btnExportDataForm_Click(object sender, EventArgs e)
        {
            ExportDataForm edf = new ExportDataForm();
            edf.FormClosed += ChildFormClosed;
            edf.Show();
        }

       

        

        
        
    }
}
