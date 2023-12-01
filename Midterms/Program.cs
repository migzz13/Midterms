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
            Console.WriteLine(" ____   ____ _____ _   _   ____  ____  _   _  ____ ____  \r\n/ ___| / ___| ____| \\ | | |  _ \\|  _ \\| | | |/ ___/ ___| \r\n\\___ \\| |  _|  _| |  \\| | | | | | |_) | | | | |  _\\___ \\ \r\n ___) | |_| | |___| |\\  | | |_| |  _ <| |_| | |_| |___) |\r\n|____/ \\____|_____|_| \\_| |____/|_| \\_\\\\___/ \\____|____/ ");
            var initialDrugs = ReadDrugsFromCsv("DrugStore Database.csv");

            var drugstore = new Drugstore(initialDrugs);

            while (true)
            {
                Console.WriteLine("\nWhat would you like to do?:");
                Console.WriteLine("1. See Inventory");
                Console.WriteLine("2. Search for a Drug");
                Console.WriteLine("3. Purchase Drug/s");
                Console.WriteLine("4. Exit");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Clear();
                    drugstore.DisplayInventory();
                }
                else if (choice == "2")
                {
                    Console.Clear();
                    Console.WriteLine("\nEnter the name or general use of the drug you want to search for:");
                    string searchTerm = Console.ReadLine();
                    drugstore.SearchDrug(searchTerm);
                }
                else if (choice == "3")
                {
                    Console.WriteLine("\nEnter the name of the drug you want to buy:");
                    string drugName = Console.ReadLine();

                    Console.WriteLine("\nEnter the quantity to buy:");
                    if (int.TryParse(Console.ReadLine(), out int quantity))
                    {
                        drugstore.BuyDrug(drugName, quantity);
                    }
                    else
                    {
                        Console.WriteLine("Invalid quantity. Please enter a valid number.");
                    }
                    Console.WriteLine("Press any key to go back");
                    Console.ReadKey();
                    Console.Clear();
                }
                else if (choice == "4")
                {
                    Console.WriteLine("See you next time!");
                    break;
                }
                else
                {
                    Console.WriteLine("\nInvalid choice. Please try again.");
                }
            }
        }

        private static List<Drug> ReadDrugsFromCsv(string filePath)
        {
            var drugs = new List<Drug>();

            try
            {
                var lines = File.ReadAllLines(filePath);

                for (int i = 1; i < lines.Length; i++)
                {
                    var values = lines[i].Split(',');

                    if (values.Length == 4)
                    {
                        string name = values[0];
                        double price = Convert.ToDouble(values[1]);
                        int quantity = Convert.ToInt32(values[2]);
                        string generalUse = values[3];

                        var drug = new Drug(name, price, quantity, generalUse);
                        drugs.Add(drug);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading drugs from CSV: {ex.Message}");
            }

            return drugs;
        }
    }
}
