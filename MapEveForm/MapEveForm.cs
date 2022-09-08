using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapEveForm
{
    public partial class MapEveForm : Form
    {
        private string subFolderPath = Function.getPathDir();
        private string nomeFile = Function.getPathFile();
        public MapEveForm()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.eve_logo_6ZK_icon;
            if (Directory.Exists(subFolderPath))
            {
                if (File.Exists(nomeFile))
                {
                    Save save = Function.ReadFromBinaryFile<Save>(nomeFile);
                    tbToken.Text = save.Token;
                }
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            string subFolderPath = Function.getPathDir();
            if (!Directory.Exists(subFolderPath)) { 
                Directory.CreateDirectory(subFolderPath);
            }
            try
            {
                Save save;
                if (File.Exists(nomeFile))
                {
                    save = Function.ReadFromBinaryFile<Save>(nomeFile);
                }
                else
                {
                    save = new Save();
                }
                save.Token = tbToken.Text;
                Function.WriteToBinaryFile(nomeFile, save);
                this.Close();

            }
            catch (Exception err) {
                Console.WriteLine("Exception: " + err.Message);
            }
        }
    }
}
