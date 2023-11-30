using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midterms
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Initialize drugs from CSV file
            var initialDrugs = ReadDrugsFromCsv("Drug Database.csv");

            // Initialize drugstore
            var drugstore = new Drugstore(initialDrugs);

            // Display initial inventory
            drugstore.DisplayInventory();

            // Example: Search for drugs
            drugstore.SearchDrug("Pain");

            // Example: Sell drugs
            drugstore.SellDrug("Paracetamol", 10);

            // Display updated inventory
            drugstore.DisplayInventory();
        }

        static List<Drug> ReadDrugsFromCsv(string filePath)
        {
            var drugs = new List<Drug>();
            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        if (values.Length == 4)
                        {
                            var name = values[0];
                            var price = double.Parse(values[1]);
                            var quantity = int.Parse(values[2]);
                            var generalUse = values[3];

                            var drug = new Drug(name, price, quantity, generalUse);
                            drugs.Add(drug);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading from CSV: {ex.Message}");
            }
            return drugs;
        }
    }
}
