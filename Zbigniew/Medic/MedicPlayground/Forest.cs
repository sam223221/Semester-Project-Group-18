using MedicPlayground;
using System;
using System.Collections.Generic;

class Forest
{
    public static List<string> MedicInventory = new List<string>();

    public void Main()
    {
        Game game = new Game();

        Console.WriteLine("Welcome to the Forest!");

        while (true)
        {
            Console.WriteLine("\nOptions:\n");
            Console.WriteLine("1. Explore the Forest");
            Console.WriteLine("2. View Medic Inventory");
            Console.WriteLine("3. Brew Potions");
            Console.WriteLine("4. Exit");

            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            Console.Clear();

            switch (choice)
            {
                case "1":
                    ExploreForest();
                    break;
                case "2":
                    ViewInventory();
                    break;
                case "3":
                    BrewPotions();
                    break;
                case "4":
                    Console.WriteLine("Exiting the Forest. Goodbye!");
                    game.Play();
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void ExploreForest()
    {
        Console.WriteLine("\nExploring the Forest...");


        if (MedicInventory.Count >= 3)
        {
            Console.WriteLine("You already have 3 herbs in your inventory. You cannot collect more.");
            return;
        }

        Console.WriteLine("You found some herbs!");

        Console.WriteLine("1. Starlight Lavender");
        Console.WriteLine("2. Mystic Mint");
        Console.WriteLine("3. Solar Chamomile");
        Console.WriteLine("4. Enigma Blossom");
        Console.WriteLine("5. Ember Fern");


        for (int i = 0; i < 3 - MedicInventory.Count; i++)
        {
            Console.Write($"Choose an herb to collect ({i + 1}/3): ");


            try
            {
                int herbChoice;
                if (int.TryParse(Console.ReadLine(), out herbChoice) && herbChoice >= 1 && herbChoice <= 5)
                {
                    string herb = GetHerbName(herbChoice);
                    if (!MedicInventory.Contains(herb))
                    {
                        Console.WriteLine($"You collected {herb}!");
                        MedicInventory.Add(herb);
                    }
                    else
                    {
                        Console.WriteLine($"You already have {herb} in your inventory.");
                        i--; 
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                    i--; 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                i--; 
            }

            Console.Clear(); 
        }
    }

    static void BrewPotions()
    {
        Console.WriteLine("\nBrewing Potions...");

        if (MedicInventory.Count < 3)
        {
            Console.WriteLine("You need at least 3 herbs to brew potions.");
            return;
        }

        if (MedicInventory.Contains("Starlight Lavender") && MedicInventory.Contains("Mystic Mint") && MedicInventory.Contains("Solar Chamomile"))
        {
            Console.WriteLine("You brewed Serenity Soothe Syrup!");
            MedicInventory.Remove("Starlight Lavender");
            MedicInventory.Remove("Mystic Mint");
            MedicInventory.Remove("Solar Chamomile");
            MedicInventory.Add("Serenity Soothe Syrup");
        }
        else if (MedicInventory.Contains("Starlight Lavender") && MedicInventory.Contains("Solar Chamomile"))
        {
            Console.WriteLine("You brewed Breathe Easy Blend!");
            MedicInventory.Remove("Starlight Lavender");
            MedicInventory.Remove("Solar Chamomile");
            MedicInventory.Add("Breathe Easy Blend");
        }
        else if (MedicInventory.Contains("Mystic Mint") && MedicInventory.Contains("Enigma Blossom"))
        {
            Console.WriteLine("You brewed Fresh Start Elixir!");
            MedicInventory.Remove("Mystic Mint");
            MedicInventory.Remove("Enigma Blossom");
            MedicInventory.Add("Fresh Start Elixir");
        }
        else if (MedicInventory.Contains("Solar Chamomile") && MedicInventory.Contains("Ember Fern"))
        {
            Console.WriteLine("You brewed Herbal Harmony Potion!");
            MedicInventory.Remove("Solar Chamomile");
            MedicInventory.Remove("Ember Fern");
            MedicInventory.Add("Herbal Harmony Potion");
        }
        else
        {
            Console.WriteLine("You don't have the right combination of herbs to brew a potion.");
        }

        MedicInventory.Clear();
    }

    static void ViewInventory()
    {
        Console.WriteLine("\nMedic Inventory:");

        if (MedicInventory.Count == 0)
        {
            Console.WriteLine("Your inventory is empty.");
        }
        else
        {
            foreach (var herb in MedicInventory)
            {
                Console.WriteLine($"- {herb}");
            }
        }
    }

    static string GetHerbName(int choice)
    {
        switch (choice)
        {
            case 1:
                return "Starlight Lavender";
            case 2:
                return "Mystic Mint";
            case 3:
                return "Solar Chamomile";
            case 4:
                return "Enigma Blossom";
            case 5:
                return "Ember Fern";
            default:
                return "Unknown Herb";
        }
    }
}
