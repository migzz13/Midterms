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
        private int LowStockThreshold = 30;
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
                Console.WriteLine($"{drug.Name} - Price: {drug.Price}, Quantity: {drug.Quantity}, Used For: {drug.GeneralUse}");
            }
            Console.WriteLine("------------------");
        }

        public void SearchDrug(string search)
        {
            Console.Clear();
            var results = drugs
                .Where(drug => drug.Name.ToLower().Contains(search.ToLower()) || drug.GeneralUse.ToLower().Contains(search.ToLower()))
                .ToList();

            if (results.Any())
            {
                Console.WriteLine($"Search results for '{search}':");
                Console.WriteLine("------------------");
                foreach (var drug in results)
                {
                    Console.WriteLine($"{drug.Name} - Price: {drug.Price}, Quantity: {drug.Quantity}, Used For: {drug.GeneralUse}");
                }
                Console.WriteLine("------------------");
            }
            else
            {
                Console.WriteLine($"No results found for '{search}'.");
            }
        }

        public void BuyDrug(string drugName, int quantity)
        {
            Drug drug = null;
            foreach (var d in drugs)
            {
                if (d.Name.ToLower() == drugName.ToLower())
                {
                    drug = d;
                    break;
                }
            }

            if (drug != null)
            {
                if (drug.Quantity > 0)
                {
                    if (drug.Quantity >= quantity)
                    {
                        double totalPrice = drug.Price * quantity;

                        drug.UpdateQuantity(-quantity);

                        UpdateStock(drugName, drug.Quantity);

                        Console.WriteLine($"Successfully bought {quantity} {drugName}(s).");
                        Console.WriteLine($"Total Price: {totalPrice}");

                        if (drug.Quantity <= LowStockThreshold)
                        {
                            Console.WriteLine($"Low stock alert: {drugName} is running low.");
                        }

                        WriteReceipt(drugName, quantity, totalPrice);
                    }
                    else
                    {
                        Console.WriteLine($"Not enough stock for {drugName}.");
                    }
                }
                else
                {
                    Console.WriteLine($"{drugName} is out of stock.");
                }
            }
            else
            {
                Console.WriteLine($"{drugName} not found in the inventory.");
            }
        }

        private void WriteReceipt(string drugName, int quantity, double totalPrice)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("Receipt.txt", true))
                {
                    sw.WriteLine("Receipt Details:");
                    sw.WriteLine("---------------------------");
                    sw.WriteLine($"Drug: {drugName}");
                    sw.WriteLine($"Quantity: {quantity}");
                    sw.WriteLine($"Total Price: {totalPrice}");
                    sw.WriteLine("---------------------------");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing receipt: {ex.Message}");
            }
        }

        private void UpdateStock(string drugName, int newQuantity)
        {
            try
            {
                var filePath = "DrugStore Database.csv";
                var updatedLines = new List<string>();

                using (var reader = new StreamReader(filePath))
                {
                    while (true)
                    {
                        var line = reader.ReadLine();

                        if (line == null)
                        {
                            break;
                        }

                        var values = line.Split(',');

                        if (values.Length == 4 && values[0].ToLower() == drugName.ToLower())
                        {
                            updatedLines.Add($"{drugName},{values[1]},{newQuantity},{values[3]}");
                        }
                        else
                        {
                            updatedLines.Add(line);
                        }
                    }
                }

                using (var writer = new StreamWriter(filePath))
                {
                    foreach (var line in updatedLines)
                    {
                        writer.WriteLine(line);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating quantity in CSV: {ex.Message}");
            }
        }
    }
}
