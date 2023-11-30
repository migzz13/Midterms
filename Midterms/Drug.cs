using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midterms
{
    public class Drug
    {
        public string Name;
        public double Price;
        public int Quantity;
        public string GeneralUse;

        public Drug(string name, double price, int quantity, string generalUse)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
            GeneralUse = generalUse;
        }

        public void UpdateQuantity(int amount)
        {
            Quantity += amount;
        }
    }
}
