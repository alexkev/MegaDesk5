using Newtonsoft.Json;
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
        private Desk.DesktopMaterial materials;

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
            var quotesFile = @"quotes.json";
            using (StreamReader reader = new StreamReader(quotesFile))
            {
                // load existing quotes
                string quotes = reader.ReadToEnd();
                List<DeskQuote> deskQuotes = JsonConvert.DeserializeObject<List<DeskQuote>>(quotes);

                dataGridView2.DataSource = deskQuotes.Select(d => new
                {
                    Date = d.QuoteDate,
                    Customer = d.CustomerName,
                    Width = d.Desk.Width,
                    Depth = d.Desk.Depth,
                    NumberOfDrawers = d.Desk.NumDrawers,
                    SurfaceMaterials = d.Desk.Material,
                    DeliveryType = d.NumShippingDays,
                    QuotePrice = d.Quote
                }).ToList();
            }

        }

        private void comSearchMaterials(object sender, EventArgs e)
        {
            var desktopMaterial = Enum.GetName(typeof(Desk.DesktopMaterial), comSearchMaterial.SelectedIndex).ToString();
            System.Diagnostics.Debug.WriteLine(desktopMaterial);

            var quotesFile = @"quotes.json";
            using (StreamReader reader = new StreamReader(quotesFile))
            {
                // load existing quotes
                string quotes = reader.ReadToEnd();
                List<DeskQuote> deskQuotes = JsonConvert.DeserializeObject<List<DeskQuote>>(quotes);

                dataGridView2.DataSource = deskQuotes.Select(d => new
                {
                    Date = d.QuoteDate,
                    Customer = d.CustomerName,
                    Width = d.Desk.Width,
                    Depth = d.Desk.Depth,
                    NumberOfDrawers = d.Desk.NumDrawers,
                    SurfaceMaterials = d.Desk.Material,
                    DeliveryType = d.NumShippingDays,
                    QuotePrice = d.Quote
                })
                .Where(q => q.SurfaceMaterials.ToString() == desktopMaterial)
                .ToList();
            }
        }
    }
}
