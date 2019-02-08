using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MegaDesk
{
    public partial class AddQuote : Form
    {
        public AddQuote()
        {
            InitializeComponent();

            var materials = Enum.GetValues(typeof(Desk.DesktopMaterial))
                            .Cast<Desk.DesktopMaterial>()
                            .ToList();

            comDesktopMaterials.DataSource = materials;

            comDesktopMaterials.SelectedIndex = -1;
        }


        private void btnSubmit_Click(object sender, EventArgs e)
        {
            var desk = new Desk();
            var deskQuote = new DeskQuote();
            try
            {
                desk.Width = width.Value;
                desk.Depth = width.Value;
                desk.NumDrawers = int.Parse(numDrawers.SelectedItem.ToString());
                desk.Material = (Desk.DesktopMaterial)comDesktopMaterials.SelectedValue;

                deskQuote.Desk = desk;
                deskQuote.CustomerName = txtCustomerName.Text;
                deskQuote.NumShippingDays = int.Parse(comShipping.SelectedItem.ToString());
                deskQuote.QuoteDate = DateTime.Now;
                deskQuote.ShippingCost = deskQuote.GetShippingCost();
                deskQuote.StructureCost = deskQuote.GetStructureCost();
                deskQuote.SurfaceCost = deskQuote.GetSurfaceCost();
                deskQuote.Quote = deskQuote.GetQuote();
        }
            catch (Exception ex)
            {

            }
            lblStructureCost.Text = deskQuote.GetStructureCost().ToString();
            lblSurfaceCost.Text = deskQuote.GetSurfaceCost().ToString();
            lblShippingCost.Text = deskQuote.GetShippingCost().ToString();
            lblTotalCost.Text = deskQuote.GetQuote().ToString();



            List<DeskQuote> deskQuotes = new List<DeskQuote>();
            if (!File.Exists(@"quotes.json"))
            {
                deskQuotes.Add(deskQuote);
                var list = JsonConvert.SerializeObject(deskQuotes);
                File.WriteAllText(@"quotes.json", JsonConvert.SerializeObject(deskQuotes));
            }
            else
            {
                using (StreamReader reader = new StreamReader(@"quotes.json"))
                {
                    string allQuotes = reader.ReadToEnd();
                    deskQuotes = JsonConvert.DeserializeObject<List<DeskQuote>>(allQuotes);
                }
                deskQuotes.Add(deskQuote);
                var list = JsonConvert.SerializeObject(deskQuotes);
                File.WriteAllText(@"quotes.json", list);
            }

            var mainMenu = (MainMenu)Tag;
            mainMenu.Show();
        }

        private void AddQuotetoFile(List<DeskQuote> deskQuotes)
        {
            File.WriteAllText(@"quotes.json", JsonConvert.SerializeObject(deskQuotes));
            /**
            string quotesFile = "quotes.txt";

            using (StreamWriter streamwriter = File.AppendText(quotesFile))
            { 
                streamwriter.WriteLine(
                $"{deskQuote.CustomerName}, " +
                $"{deskQuote.QuoteDate}, " +
                $"{deskQuote.Desk.Depth}, " +
                $"{deskQuote.Desk.Width}, " +
                $"{deskQuote.Desk.NumDrawers}, " +
                $"{deskQuote.Desk.Material}, " +
                $"{deskQuote.NumShippingDays} Days, " +
                $"{deskQuote.Quote}");
            }
            **/

            // serialize JSON to a string and then write string to a file
        }
       
        private void cancelQuoteButton(object sender, EventArgs e)
        {
            var mainMenu = (MainMenu)Tag;
            mainMenu.Show();
            Close();
        }

        private void txtCustomerName_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void comDesktopMaterials_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void numDrawers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void width_ValueChanged(object sender, EventArgs e)
        {

        }

        private void depth_ValueChanged(object sender, EventArgs e)
        {

        }

        private void lblTotalCost_Click(object sender, EventArgs e)
        {
            
        }
    }
}
