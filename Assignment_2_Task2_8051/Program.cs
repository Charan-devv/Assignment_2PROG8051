using System;
using System.Collections.Generic;

// 1. Interface defining the electrician's functionality
interface IElectricianTask
{
    void PerformInstallation();
    void DisplayCustomerInfo();
}

// 2. Abstract base class implementing the interface
abstract class CustomerBase : IElectricianTask
{
    public string Name { get; set; }
    public string BuildingType { get; set; }
    public int Size { get; set; }
    public int LightBulbs { get; set; }
    public int Outlets { get; set; }
    public string CreditCard { get; set; }

    protected CustomerBase(string name, string buildingType, int size, int lightBulbs, int outlets, string creditCard)
    {
        Name = name;
        BuildingType = buildingType;
        Size = Math.Max(1000, Math.Min(50000, size)); // Enforce size limits
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

    public virtual void DisplayCustomerInfo()
    {
        Console.WriteLine($"{Name}, {BuildingType}, {Size} sqft, {LightBulbs} bulbs, {Outlets} outlets, Card: {CreditCard}");
    }

    public virtual void PerformInstallation()
    {
        Console.WriteLine($"Creating wiring schema for {Name} ({BuildingType})...");
        Console.WriteLine("Purchasing necessary parts...");
    }

    // Abstract method for supplementary tasks
    public abstract void PerformSupplementaryTask();
}

// 3. Derived class for House
class HouseCustomer : CustomerBase
{
    public HouseCustomer(string name, int size, int lightBulbs, int outlets, string creditCard)
        : base(name, "House", size, lightBulbs, outlets, creditCard) { }

    public override void PerformSupplementaryTask()
    {
        Console.WriteLine("Installing fire alarms...");
    }
}

// 3. Derived class for Barn
class BarnCustomer : CustomerBase
{
    public BarnCustomer(string name, int size, int lightBulbs, int outlets, string creditCard)
        : base(name, "Barn", size, lightBulbs, outlets, creditCard) { }

    public override void PerformSupplementaryTask()
    {
        Console.WriteLine("Wiring milking equipment...");
    }
}

// 3. Derived class for Garage
class GarageCustomer : CustomerBase
{
    public GarageCustomer(string name, int size, int lightBulbs, int outlets, string creditCard)
        : base(name, "Garage", size, lightBulbs, outlets, creditCard) { }

    public override void PerformSupplementaryTask()
    {
        Console.WriteLine("Installing automatic doors...");
    }
}

class Program
{
    static void Main()
    {
        List<CustomerBase> customers = new List<CustomerBase>();

        while (true)
        {
            Console.Write("Enter customer name (or type 'exit' to finish): ");
            string name = Console.ReadLine();
            if (name.ToLower() == "exit") break;

            Console.Write("Enter building type (House/Barn/Garage): ");
            string buildingType = Console.ReadLine().ToLower();

            Console.Write("Enter size of building (1000 - 50000 sqft): ");
            int size = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter number of light bulbs (max 20): ");
            int bulbs = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter number of outlets (max 50): ");
            int outlets = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter credit card number (16-digit): ");
            string creditCard = Console.ReadLine();

            CustomerBase customer = buildingType switch
            {
                "house" => new HouseCustomer(name, size, bulbs, outlets, creditCard),
                "barn" => new BarnCustomer(name, size, bulbs, outlets, creditCard),
                "garage" => new GarageCustomer(name, size, bulbs, outlets, creditCard),

            };

            if (customer != null)
            {
                customers.Add(customer);
                Console.WriteLine("Installation process:");
                customer.PerformInstallation();
                customer.PerformSupplementaryTask();
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Invalid building type. Please enter House, Barn, or Garage.");
            }
        }

        Console.WriteLine("\nSummary of all customers:");
        foreach (var customer in customers)
        {
            customer.DisplayCustomerInfo();
        }
    }
}
