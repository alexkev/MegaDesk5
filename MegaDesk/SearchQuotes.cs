using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MegaDesk
{
    public partial class SearchQuotes : Form
    { 
        public SearchQuotes()
        {
            InitializeComponent();
            loadGrid();

           var materials = Enum.GetValues(typeof(Desk.DesktopMaterial))
                            .Cast<Desk.DesktopMaterial>()
                            .ToList();

           comSearchMaterial.DataSource = materials;

           comSearchMaterial.SelectedIndex = -1;
        }
        private void cancelQuoteButton(object sender, EventArgs e)
        {
            var mainMenu = (MainMenu)Tag;
            mainMenu.Show();
            Close();
        }

        private void loadGrid()
        {
            try
            {
                string[] deskQuotes = File.ReadAllLines(@"quotes.txt");
                foreach (string deskQuote in deskQuotes)
                {
                    string[] arrRow = deskQuote.Split(new char[] { ',' });
                    dataGridView2.Rows.Add(arrRow);
                }

            }
            catch (FileNotFoundException)
            {

            }

        }

        private void comSearchMaterials(object sender, EventArgs e)
        {
            string search = comSearchMaterial.SelectedValue.ToString();
            System.Diagnostics.Debug.Write(search);

            dataGridView2.Rows.Clear();

            string[] deskQuotes = File.ReadAllLines(@"quotes.txt");
            foreach (string deskQuote in deskQuotes)
            {
                string[] arrRow = deskQuote.Split(new char[] { ',' });
                if (deskQuote.Contains(search))
                {
                    dataGridView2.Rows.Add(arrRow);
                }
            }
        }
    }
}
