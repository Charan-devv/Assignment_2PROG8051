using System;
using System.Collections.Generic;

// Class to represent a Customer
class Customer
{
    public string Name { get; set; }
    public string BuildingType { get; set; }
    public int Size { get; set; }
    public int LightBulbs { get; set; }
    public int Outlets { get; set; }
    public string CreditCard { get; set; }

    public Customer(string name, string buildingType, int size, int lightBulbs, int outlets, string creditCard)
    {
        Name = name;
        BuildingType = buildingType;
        Size = size;
        LightBulbs = Math.Min(lightBulbs, 20); // Max limit of 20
        Outlets = Math.Min(outlets, 50); // Max limit of 50
        CreditCard = MaskCreditCard(creditCard);
    }

    // Method to mask credit card number
    private string MaskCreditCard(string cardNumber)
    {
        if (cardNumber.Length == 16)
        {
            return cardNumber.Substring(0, 4) + " XXXX XXXX " + cardNumber.Substring(12, 4);
        }
        return "Invalid Card";
    }

    // Method to display customer details
    public void DisplayCustomerInfo()
    {
        Console.WriteLine($"{Name}, {BuildingType}, {Size} sqft, {LightBulbs} bulbs, {Outlets} outlets, Card: {CreditCard}");
    }

    // Method to perform specific installation
    public void PerformInstallation()
    {
        Console.WriteLine($"Creating wiring schema for {Name} ({BuildingType})...");
        Console.WriteLine("Purchasing necessary parts...");

        switch (BuildingType.ToLower())
        {
            case "house":
                Console.WriteLine("Installing fire alarms...");
                break;
            case "barn":
                Console.WriteLine("Wiring milking equipment...");
                break;
            case "garage":
                Console.WriteLine("Installing automatic doors...");
                break;
            default:
                Console.WriteLine("Unknown building type. No special installations required.");
                break;
        }
    }
}

class Program
{
    static void Main()
    {
        List<Customer> customers = new List<Customer>();

        while (true)
        {
            Console.Write("Enter customer name (or type 'exit' to finish): ");
            string name = Console.ReadLine();
            if (name.ToLower() == "exit") break;

            Console.Write("Enter building type (House/Barn/Garage): ");
            string buildingType = Console.ReadLine();

            Console.Write("Enter size of building (1000 - 50000 sqft): ");
            int size = Math.Max(1000, Math.Min(50000, Convert.ToInt32(Console.ReadLine())));

            Console.Write("Enter number of light bulbs (max 20): ");
            int bulbs = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter number of outlets (max 50): ");
            int outlets = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter credit card number (16-digit): ");
            string creditCard = Console.ReadLine();

            Customer customer = new Customer(name, buildingType, size, bulbs, outlets, creditCard);
            customers.Add(customer);

            Console.WriteLine("Installation process:");
            customer.PerformInstallation();
            Console.WriteLine();
        }

        Console.WriteLine("\nSummary of all customers:");
        foreach (var customer in customers)
        {
            customer.DisplayCustomerInfo();
        }
    }
}