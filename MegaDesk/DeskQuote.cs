using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaDesk
{
    public class DeskQuote
    {
        public string CustomerName { get; set; }
        public int NumShippingDays { get; set; }
        public DateTime QuoteDate { get; set; }
        public decimal Quote { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal StructureCost { get; set; }
        public decimal SurfaceCost { get; set; }
        public Desk Desk { get; set; }

        public decimal GetQuote()
        {
            Quote = GetShippingCost() + GetStructureCost() + GetSurfaceCost();
            return Quote;
        }

        public decimal GetShippingCost()
        {
            decimal size = Desk.Width * Desk.Depth;
            switch (NumShippingDays)
            {
                case 3:
                    ShippingCost = 60;
                    break;
                case 5:
                    ShippingCost = 40;
                    break;
                case 7:
                    ShippingCost = 30;
                    break;
                case 14:
                    ShippingCost = 0;
                    break;
                default:
                    break;
            }

            if (size < 1000)
                return ShippingCost;
            else if (size > 1000 && size < 2000)
                ShippingCost += 10;
            else if (size > 2000)
                ShippingCost += 20;

            return ShippingCost;
        }

        public decimal GetStructureCost()
        {
            StructureCost = (Desk.NumDrawers * 50) + 200;
            return StructureCost;
        }

        public decimal GetSurfaceCost()
        {
            decimal size = Desk.Width + Desk.Depth;

            switch (Desk.Material)
            {
                case Desk.DesktopMaterial.Rosewood:
                    SurfaceCost = 300;
                    break;
                case Desk.DesktopMaterial.Laminate:
                    SurfaceCost = 100;
                    break;
                case Desk.DesktopMaterial.Veneer:
                    SurfaceCost = 125;
                    break;
                case Desk.DesktopMaterial.Oak:
                    SurfaceCost = 200;
                    break;
                case Desk.DesktopMaterial.Pine:
                    SurfaceCost = 50;
                    break;
                default:
                    break;
            }

            if (size > 1000)
                SurfaceCost += size;
            else
                SurfaceCost += 0;

            return SurfaceCost;
        }
    }
 }