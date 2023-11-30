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
            var initialDrugs = ReadDrugsFromCsv("Drug Database.csv");

            // Initialize drugstore
            var drugstore = new Drugstore(initialDrugs);

            while (true)
            {
                Console.WriteLine("\nPlease choose an option:");
                Console.WriteLine("1. Display Inventory");
                Console.WriteLine("2. Search for a Drug");
                Console.WriteLine("3. Sell Drug");
                Console.WriteLine("4. Exit");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Clear(); // Clear console before displaying inventory
                    drugstore.DisplayInventory();
                }
                else if (choice == "2")
                {
                    Console.WriteLine("\nEnter the name or general use of the drug you want to search for:");
                    string searchTerm = Console.ReadLine();
                    drugstore.SearchDrug(searchTerm);
                }
                else if (choice == "3")
                {
                    Console.WriteLine("\nEnter the name of the drug you want to sell:");
                    string drugName = Console.ReadLine();

                    Console.WriteLine("\nEnter the quantity to sell:");
                    if (int.TryParse(Console.ReadLine(), out int quantity))
                    {
                        drugstore.SellDrug(drugName, quantity);
                    }
                    else
                    {
                        Console.WriteLine("Invalid quantity. Please enter a valid number.");
                    }
                }
                else if (choice == "4")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\nInvalid choice. Please try again.");
                }
            }
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
