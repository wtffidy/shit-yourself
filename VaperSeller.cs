//cs_include Scripts/CoreBots.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class Test
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    // Script configuration and options
    public bool DontPreconfigure = true;
    public string OptionsStorage = "VapeDistributionOptions";
    public List<IOption> Options = new()
    {
        new Option<Flavor>("vapeFlavor", "Vape Flavor", "Select the vape flavor to add to inventory.", Flavor.Apple),
        new Option<double>("vapePrice", "Vape Price", "Default price for each vape.", 19.99),
        new Option<int>("orderQuantity", "Order Quantity", "Number of vapes to add to each order.", 5),
        CoreBots.Instance.SkipOptions,
    };

    // List to hold vapes inventory
    private List<Vape> vapesInventory = new List<Vape>();

    // Main script entry point
    public void ScriptMain(IScriptInterface bot)
    {
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "vapes_inventory.txt");

        // Delete the file if it exists before starting
        if (File.Exists(filePath))
        {
            try
            {
                File.Delete(filePath);
                Core.Logger($"Deleted existing file '{filePath}'.");
            }
            catch (Exception ex)
            {
                Core.Logger($"Error deleting file '{filePath}': {ex.Message}");
            }
        }

        Core.SetOptions();

        // Load existing inventory from file
        LoadInventoryFromFile();

        SetupOrder();

        // Export updated inventory to file
        ExportInventoryToFile();

        Core.SetOptions(false);

        // Read inventory from file and display
        ReadInventoryFromFile();
    }


    // Method to setup the vape inventory and process orders
    public void SetupOrder()
    {
        // Read options from configuration
        Flavor vapeFlavorOption = Bot.Config.Get<Flavor>("vapeFlavor");
        double vapePriceOption = Bot.Config.Get<double>("vapePrice");
        int orderQuantityOption = Bot.Config.Get<int>("orderQuantity");

        // Find existing vape in inventory or create new if not found
        Vape existingVape = vapesInventory.FirstOrDefault(v => v.Flavor == vapeFlavorOption);
        if (existingVape == null)
        {
            // Create new vape and add to inventory
            existingVape = new Vape(vapeFlavorOption, vapePriceOption);
            vapesInventory.Add(existingVape);
        }

        // Increase demand for the selected vape flavor
        existingVape.IncreaseDemand(orderQuantityOption);
        existingVape.CalculateTotalPrice(); // Calculate total price after updating demand

        // Display current inventory
        Core.Logger("Current Inventory:");
        foreach (var vape in vapesInventory)
        {
            Core.Logger(vape.ToString());
        }

        // Process order for the specified quantity
        Order(vapesInventory, orderQuantityOption);
    }

    // Method to process an order for vapes
    private void Order(List<Vape> inventory, int orderQuantity)
    {
        Core.Logger("Processing Order:");
        for (int i = 0; i < orderQuantity; i++)
        {
            if (i < inventory.Count)
            {
                Core.Logger($"Adding {inventory[i].Name} to order.");
                // Logic for processing order (placeholder)
            }
        }
        Core.Logger("Order processed successfully.");
    }

    // Method to log inventory details including demands and total prices to a file
    private void ExportInventoryToFile()
    {
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "vapes_inventory.txt");

        using (StreamWriter writer = new StreamWriter(filePath, false))
        {
            double grandTotal = 0;

            writer.WriteLine("Inventory Details:");
            foreach (var vape in vapesInventory)
            {
                // Calculate total price for each vape (sell price * demand)
                vape.CalculateTotalPrice();

                // Log vape details including demand and total price to file
                writer.WriteLine($"{vape.Name} - Demand: {vape.Demand}, Total Price: ${vape.TotalPrice:F2}");

                // Accumulate grand total
                grandTotal += vape.TotalPrice;
            }

            // Log grand total for the order to file
            writer.WriteLine($"Grand Total for Order: ${grandTotal:F2}");
            writer.WriteLine(); // Blank line for separation or next entry
        }

        Core.Logger("Inventory details exported to vapes_inventory.txt.");
    }

    // Method to read inventory from file and update vapes inventory list
    private void LoadInventoryFromFile()
    {
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "vapes_inventory.txt");

        try
        {
            if (File.Exists(filePath))
            {
                vapesInventory.Clear(); // Clear existing inventory before loading

                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.StartsWith("Inventory Details:") || string.IsNullOrWhiteSpace(line))
                            continue;

                        string[] parts = line.Split('-', ',');
                        if (parts.Length >= 2)
                        {
                            string vapeName = parts[0].Trim();
                            string demandStr = parts[1].Trim();
                            if (int.TryParse(demandStr.Replace("Demand:", "").Trim(), out int demand))
                            {
                                // Find existing vape in inventory or create new if not found
                                Flavor vapeFlavor = (Flavor)Enum.Parse(typeof(Flavor), vapeName);
                                Vape existingVape = vapesInventory.FirstOrDefault(v => v.Flavor == vapeFlavor);
                                if (existingVape == null)
                                {
                                    existingVape = new Vape(vapeFlavor, 0); // Price is 0, will be updated later
                                    vapesInventory.Add(existingVape);
                                }
                                existingVape.Demand = demand; // Update demand
                            }
                        }
                    }
                }

                Core.Logger("Inventory loaded from file successfully.");

                // Delete the file after loading
                File.Delete(filePath);
                Core.Logger($"File '{filePath}' deleted after loading inventory.");
            }
            else
            {
                Core.Logger("Inventory file not found. Starting with empty inventory.");
            }
        }
        catch (Exception ex)
        {
            Core.Logger($"Error loading inventory from file: {ex.Message}");
        }
    }

    // Method to read inventory from file and display
    private void ReadInventoryFromFile()
    {
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "vapes_inventory.txt");

        try
        {
            Core.Logger("(ReadInventoryFromFile) Reading Inventory from File:");

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Core.Logger(line); // Output each line from the file
                }
            }

            Core.Logger("Inventory read from file successfully.");

            // Delete the file after reading
            File.Delete(filePath);
            Core.Logger($"File '{filePath}' deleted successfully.");
        }
        catch (FileNotFoundException)
        {
            Core.Logger($"Error: File '{filePath}' not found.");
        }
        catch (Exception ex)
        {
            Core.Logger($"Error reading inventory from file: {ex.Message}");
        }
    }

     public enum Flavor
    {
        Acai,
        AloeVera,
        Almond,
        Aniseed,
        Apple,
        Apricot,
        Banana,
        Berry,
        Biscuit,
        Blackberry,
        Blackcurrant,
        BloodOrange,
        Blueberry,
        BlueRaspberry,
        Bubblegum,
        Butterscotch,
        Cake,
        Candy,
        Caramel,
        Cereal,
        Cheesecake,
        Cherry,
        Chocolate,
        Cigar,
        Cinnamon,
        Citrus,
        Cocktail,
        Coconut,
        Cola,
        Coffee,
        Cookie,
        CottonCandy,
        Cranberry,
        Cream,
        Cucumber,
        Custard,
        Donuts,
        Elderflower,
        EnergyDrink,
        Eucalyptus,
        Fruits,
        GoldenTobacco,
        GrahamCracker,
        Grape,
        Grapefruit,
        Guava,
        GummyBear,
        Hazelnut,
        Heisenberg,
        Honey,
        Ice,
        IceCream,
        Jam,
        Jackfruit,
        Kiwi,
        Lemonade,
        Lemon,
        Lime,
        Lychee,
        Mango,
        Marshmallow,
        Melon,
        Menthol,
        Meringue,
        Milk,
        MillionsSweets,
        Mint,
        Nuts,
        Orange,
        Papaya,
        PassionFruit,
        Pastry,
        Peach,
        PeanutButter,
        Pear,
        Peppermint,
        Pineapple,
        PinkLemonade,
        Pomegranate,
        Popcorn,
        Raspberry,
        Rhubarb,
        Scone,
        Sherbet,
        Shisha,
        Slushie,
        Soda,
        Sorbet,
        Spearmint,
        Strawberry,
        Tobacco,
        Tropical,
        Vanilla,
        VirginiaTobacco,
        Waffle,
        Watermelon,
        Yoghurt,
        Unknown // Placeholder for unknown flavor or default
    }


    public class Vape
    {
        public string Name { get; set; }
        public Flavor Flavor { get; set; }
        private double price; // Private backing field for price

        public double Price
        {
            get { return price; }
            set
            {
                if (value >= 0) // Example validation (ensure price is non-negative)
                {
                    price = value;
                    CalculateTotalPrice(); // Recalculate total price whenever price changes
                }
                else
                {
                    // Handle invalid price input (throw exception, log error, etc.)
                    throw new ArgumentException("Price cannot be negative.");
                }
            }
        }

        public int demand; // Private backing field for demand
        public int Demand
        {
            get { return demand; }
            set
            {
                if (value >= 0) // Example validation (ensure demand is non-negative)
                {
                    demand = value;
                    CalculateTotalPrice(); // Recalculate total price whenever demand changes
                }
                else
                {
                    // Handle invalid demand input (throw exception, log error, etc.)
                    throw new ArgumentException("Demand cannot be negative.");
                }
            }
        }

        public double TotalPrice { get; private set; } // Private setter for total price

        // Constructor to initialize vape properties
        public Vape(Flavor flavor, double price)
        {
            Flavor = flavor;
            Name = flavor.ToString();
            Price = price; // Initialize price using property setter
            Demand = 0; // Initialize demand to 0
            CalculateTotalPrice(); // Calculate total price initially
        }

        // Method to increase demand for a vape item
        public void IncreaseDemand(int quantity)
        {
            Demand += quantity; // Increase demand by specified quantity
            CalculateTotalPrice(); // Recalculate total price after demand change
        }

        // Method to calculate total price for the vape
        public void CalculateTotalPrice()
        {
            TotalPrice = Price * Demand;
        }

        // Method to format vape details as a string
        public override string ToString()
        {
            return $"{Name} - {Flavor} - ${Price:F2} - Demand: {Demand}, Total Price: ${TotalPrice:F2}";
        }
    }

}
