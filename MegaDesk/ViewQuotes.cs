using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MegaDesk
{
    public partial class ViewQuotes : Form
    {
        public ViewQuotes()
        {
            InitializeComponent();
            loadGrid();
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
                using (StreamReader reader = new StreamReader(quotesFile)) {
                    // load existing quotes
                    string quotes = reader.ReadToEnd();
                    List<DeskQuote> deskQuotes = JsonConvert.DeserializeObject<List<DeskQuote>>(quotes);

                    dataGridView1.DataSource = deskQuotes.Select(d => new
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
