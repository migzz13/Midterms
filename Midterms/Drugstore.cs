using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midterms
{
    public class Drugstore
    {
        private List<Drug> drugs;

        public Drugstore(List<Drug> initialDrugs)
        {
            drugs = initialDrugs;
        }

        public void DisplayInventory()
        {
            Console.WriteLine("Current Inventory:");
            Console.WriteLine("------------------");
            foreach (var drug in drugs)
            {
                Console.WriteLine($"{drug.Name} - Price: {drug.Price}, Quantity: {drug.Quantity}, General Use: {drug.GeneralUse}");
            }
            Console.WriteLine("------------------");
        }

        public void SearchDrug(string searchTerm)
        {
            var results = drugs
                .Where(drug => drug.Name.ToLower().Contains(searchTerm.ToLower()) || drug.GeneralUse.ToLower().Contains(searchTerm.ToLower()))
                .ToList();

            if (results.Any())
            {
                Console.WriteLine($"Search results for '{searchTerm}':");
                foreach (var drug in results)
                {
                    Console.WriteLine($"{drug.Name} - Price: {drug.Price}, Quantity: {drug.Quantity}, General Use: {drug.GeneralUse}");
                }
            }
            else
            {
                Console.WriteLine($"No results found for '{searchTerm}'.");
            }
        }

        public void SellDrug(string drugName, int quantity)
        {
            var drug = drugs.FirstOrDefault(d => d.Name.ToLower() == drugName.ToLower());

            if (drug != null)
            {
                if (drug.Quantity >= quantity)
                {
                    drug.UpdateQuantity(-quantity);
                    Console.WriteLine($"Successfully sold {quantity} {drugName}(s).");
                }
                else
                {
                    Console.WriteLine($"Not enough stock for {drugName}.");
                }
            }
            else
            {
                Console.WriteLine($"{drugName} not found in the inventory.");
            }
        }
    }
}
