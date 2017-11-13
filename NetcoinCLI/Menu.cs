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
        string methodResult;
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
            var accountId = 0;
            var sum = 0;
            input = input.ToLower();
            try
            {
                switch (input)
                {
                    case "1":
                    case "sök kund":
                        Console.Write("\nSök efter kund genom att skriva in namn på kund eller postort som kunden bor på: ");
                        var searchString = Console.ReadLine();
                        var resultListFromSearch = _bankSystem.GetCustomerByNameOrArea(searchString);
                        if (resultListFromSearch.Count != 0)
                        {
                            var counter = 0;
                            foreach (var customer in resultListFromSearch)
                            {
                                counter++;
                                Console.WriteLine($"{counter}. KundID: {customer.CustomerId}, Namn: {customer.Name}, Postort: {customer.Area}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Sökning hittade inga resultat.");
                        }
                        
                        break;

                    case "2":
                    case "visa kundbild":
                        Console.Write("\nSkriv in kundnummer för att få kundbild: ");
                        var result = _bankSystem.GetCustomerById(Console.ReadLine());
                        Console.WriteLine($"Kundnummer: {result.CustomerId}\n" +
                            $"Namn: {result.Name}\n" +
                            $"Organisationsnummer: {result.LegalId}\n" +
                            $"Adress: {result.Address}\n" +
                            $"Postnummer: {result.PostalCode}\n" +
                            $"Område: {result.Area}\n" +
                            $"Stad: {result.City}\n" +
                            $"Land: {result.Country}\n" +
                            $"Telefonnummer: {result.PhoneNumber}\n\n" +
                            $"Konton:\n");
                        foreach (var account in result.Accounts)
                        {
                            Console.WriteLine($"{account.AccountId}: {account.Balance} kr");
                        }
                        break;

                    case "3":
                    case "skapa kund":
                        Console.Write("\nFör in uppgifter för att skapa kund: \nNamn: ");
                        var name = Console.ReadLine();
                        Console.Write("Organisationsnummer: ");
                        var legalId = Console.ReadLine();
                        Console.Write("Gatuadress: ");
                        var address = Console.ReadLine();
                        Console.Write("Postnummer: ");
                        var postalCode = Console.ReadLine();
                        Console.Write("Stad: ");
                        var city = Console.ReadLine();
                        Console.Write("Område: ");
                        var area = Console.ReadLine();
                        Console.Write("Land: ");
                        var country = Console.ReadLine();
                        Console.Write("Telefonnummer: ");
                        var phonenumber = Console.ReadLine();
                        var isCreatedOrNot = _bankSystem.CreateCustomer(name,legalId,area,address,postalCode,city,country,phonenumber);
                        if (isCreatedOrNot)
                        {
                            Console.Write($"Kund {name}, med organistaionsnummer: {legalId} skapad");
                        }
                        else
                        {
                            Console.Write("Något gick fel när du skapade kunden. Kontakta support.");
                        }
                        break;

                    case "4":
                    case "ta bort kund":
                        Console.Write("\nSkriv in kundnummer för den du vill radera: ");
                        int customerId = int.Parse(Console.ReadLine());
                        if (_bankSystem.RemoveCustomer(customerId))
                            Console.WriteLine($"Kund med ID { customerId } med tillhörande konton har tagits bort.");
                        else
                            Console.WriteLine("Kunden kunde inte tas bort - var god kontrollera kundnummer och att dess konton har 0 i saldo.");
                        break;

                    case "5":
                    case "skapa konto":
                        Console.Write("\nAnge kundnummer där du vill att nya kontot skapas: ");
                        var accountCreatedOrNot = _bankSystem.CreateAccount(int.Parse(Console.ReadLine()));
                        if (accountCreatedOrNot)
                        {
                            Console.WriteLine("Konto skapat.");
                        }
                        else
                        {
                            Console.WriteLine("Något gick fel när konto skulle skapas, kontakta support.");
                        }
                        break;

                    case "6":
                    case "ta bort konto":
                        Console.Write("\nAnge kontonummer för det konto du vill radera: ");
                        var accountRemovedOrNot = _bankSystem.RemoveAccount(int.Parse(Console.ReadLine()));
                        if (accountRemovedOrNot)
                        {
                            Console.WriteLine("Konto raderat.");
                        }
                        else
                        {
                            Console.WriteLine("Konto inte raderat, något gick fel. kontakta support.");
                        }
                        
                        break;

                    case "7":
                    case "insättning":
                        Console.Write("\nUppge summa för insätting:");
                        sum = int.Parse(Console.ReadLine());
                        Console.Write("Uppge kontonummer som insättningen kommer gå till: ");
                        accountId = int.Parse(Console.ReadLine());
                        methodResult = _bankSystem.DepositToAccount(accountId, sum);
                        Console.WriteLine(methodResult);
                        break;

                    case "8":
                    case "uttag":
                        Console.Write("\nAnge summa som ska tas ut: ");
                        sum = int.Parse(Console.ReadLine());
                        Console.Write("Uppge vilket kontonummer ska dras från: ");
                        accountId = int.Parse(Console.ReadLine());
                        methodResult = _bankSystem.DepositToAccount(accountId,sum);
                        Console.WriteLine(methodResult);
                        break;

                    case "9":
                    case "överföring":
                        Console.Write("\nAnge summa för transaktion: ");
                        var sumForTranscation = int.Parse(Console.ReadLine());
                        Console.Write("Uppge kontonummer som ska debiteras: ");
                        var withdrawAccountId = int.Parse(Console.ReadLine());
                        Console.Write("Uppge kontonummer som ska ta emot summa: ");
                        var depositAccountId = int.Parse(Console.ReadLine());
                        bool success = _bankSystem.TransferMoneyBetweenAccounts(depositAccountId, withdrawAccountId, sumForTranscation);
                        if (success)
                        {
                            Console.WriteLine($"Skickade {sumForTranscation} från {withdrawAccountId} till {depositAccountId}");
                        }
                        else
                        {
                            Console.WriteLine("Ett fel uppstod. Vänligen granska kontonummer och belopp");
                        }
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
                        Console.WriteLine("Alternativ finns inte. Vänligen skriv \"Hjälp\" för en lista på befintliga alternativ");
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
