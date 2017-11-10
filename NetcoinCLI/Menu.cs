using System;
using System.Collections.Generic;
using System.Text;
using NetcoinDbLib;
using NetcoinLib;

namespace NetcoinCLI
{
    public class Menu
    {
        private readonly BankSystem _bankSystem;
        private bool isRunning = true;

        public Menu(BankSystem bankSystem)
        {
            _bankSystem = bankSystem;
        }

        public void MenuSelection(string[] args)
        {
            _bankSystem.ReadTextFile(args[0]);
            _bankSystem.Initialize();
            //ShowBankLogo();
            ShowCustomerAccountsStatistics();
            ShowMenu();
            while (isRunning)
            {
                HandleMenuSelection(Console.ReadLine().ToLower());
            }
        }

        public void HandleMenuSelection(string input)
        {
            var customerId = 0;
            var accountId = 0;
            var sum = 0;
            try
            {
                switch (input)
                {
                    case "1":
                        Console.Write("\nSök efter kund genom att skriva in namn på kund eller postort som kunden bor på: ");
                        var searchString = Console.ReadLine();
                        var resultListFromSearch = _bankSystem.GetCustomerByNameOrArea(searchString);
                        if (resultListFromSearch.Count != 0)
                        {
                            foreach (var customer in resultListFromSearch)
                            {
                                Console.WriteLine($"");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Sökning hittade inga resultat.");
                        }
                        
                        break;

                    case "2":
                        Console.Write("\nSkriv in kundnummer för att få kundbild: ");
                        customerId = int.Parse(Console.ReadLine());
                        //TODO Add a service to get customer info.
                        break;

                    case "3":
                        Console.Write("\nFör in uppgifter för att skapa kund: \n\tNamn: ");
                        var name = Console.ReadLine();
                        Console.Write("Personnummer: ");
                        var legalId = Console.ReadLine();
                        Console.Write("Gatuadress: ");
                        var address = Console.ReadLine();
                        Console.Write("Postnummer: ");
                        var postalCode = Console.ReadLine();
                        Console.Write("Område: ");
                        var area = Console.ReadLine();
                        //TODO Add a service to add a customer.
                        break;

                    case "4":
                        Console.Write("\nSkriv in kundnummer för den du vill radera: ");
                        customerId = int.Parse(Console.ReadLine());
                        //TODO Add a service to delete a customer.
                        break;

                    case "5":
                        Console.Write("\nAnge kundnummer där du vill att nya kontot skapas: ");
                        customerId = int.Parse(Console.ReadLine());
                        //TODO Add a service to create a account.
                        break;

                    case "6":
                        Console.Write("\nAnge kontonummer för det konto du vill radera: ");
                        accountId = int.Parse(Console.ReadLine());
                        //TODO Add a service to delete an account.
                        break;

                    case "7":
                        Console.Write("\nUppge summa för insätting:");
                        sum = int.Parse(Console.ReadLine());
                        Console.Write("Uppge kontonummer som insättningen kommer gå till: ");
                        accountId = int.Parse(Console.ReadLine());
                        //TODO Add a service to deposit money to an account.
                        break;

                    case "8":
                        Console.Write("\nAnge summa som ska tas ut: ");
                        sum = int.Parse(Console.ReadLine());
                        Console.Write("Uppge vilket kontonummer ska dras från: ");
                        accountId = int.Parse(Console.ReadLine());
                        //TODO Add a service for withdraw money from a account.
                        break;

                    case "9":
                        Console.Write("\nAnge summa för transaktion: ");
                        var sumForTranscation = int.Parse(Console.ReadLine());
                        Console.Write("Uppge kontonummer som ska debiteras: ");
                        var withdrawAccountId = int.Parse(Console.ReadLine());
                        Console.Write("Uppge kontonummer som ska ta emot summa: ");
                        var depositAccountId = int.Parse(Console.ReadLine());
                        //TODO Add a service for transaction between accounts.
                        break;
                    case "hjälp":
                        ShowMenu();
                        break;
                    case "rensa":
                        Console.Clear();
                        break;
                    case "avsluta":
                        isRunning = false;
                        _bankSystem.SaveTextFile();
                        break;
                    default:
                        Console.WriteLine("Alternativ finns inte");
                        break;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Kommando var felaktigt.");
            }
        }

        public void ShowMenu()
        {
            Console.WriteLine("\n\n\n HUVUDMENY" +
                              "\n 1) Sök kund" +
                              "\n 2) Visa kundbild" +
                              "\n 3) Skapa kund" +
                              "\n 4) Ta bort kund" +
                              "\n 5) Skapa konto" +
                              "\n 6) Ta bort konto" +
                              "\n 7) Insättning" +
                              "\n 8) Uttag" +
                              "\n 9) Överföring" +
                              "\n\n Skriv hjälp för att visa menu igen." +
                              "\n Skriv rensa för att tömma skärmen." +
                              "\n Skriv avsluta för att avsluta programmet.");
        }
        private void ShowCustomerAccountsStatistics()
        {
            Console.WriteLine($"Läser in bankdata.txt..." +
                              $"\n Antal kunder: {_bankSystem.Customers.Count} " +
                              $"\n Antal konton: {_bankSystem.Accounts.Count} " +
                              $"\n Totalt saldo: {_bankSystem.TotalBalance}"); 
        }

        public void ShowBankLogo()
        {

            //TODO If there is time, fix ASCII art.
            //Console.WriteLine(" _   _  _____  _____  _____  _____  _____  _   _ ______   ___   _   _  _   __");
            //Console.WriteLine("| \\ | || ___ || _   _ |/ __ \\| _ || _   _ || \\ | || ___ \\ / _ \\ | \\ | || | / /");
            //Console.WriteLine("|  \\| || | __ | |  | /  \\/| | | |  | |  |  \\| || | _ / // /_\\ \\|  \\| || |/ /");
            //Console.WriteLine("| . ` || __ |   | |  | |    | | | |  | |  | . ` || ___ \\| _ || . ` ||    \\ ");
            //Console.WriteLine("| |\\  || | ___ | |  | \\__ /\\ \\_ / / _ | | _ | |\\  || | _ / /| | | || |\\  || |\\  \\");
            //Console.WriteLine("\\_ | \\_ /\\____ /   \\_ /   \\____ / \\___ /  \\___ / \\_ | \\_ /\\____ / \\_ | | _ /\\_ | \\_ /\\_ | \\_ /");
        }
    }
}
