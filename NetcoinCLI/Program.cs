using System;

namespace NetcoinCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO Fix a Logo for bank in ASCII.           
            var isRunning = true;
            Console.WriteLine($"\tLäser in bankdata.txt..." +
                                                   $"\n\t Antal kunder: {1} " +
                                                   $"\n\t Antal konton: {2} " +
                                                   $"\n\t Totalt saldo: {3}");
            while (isRunning)
            {
                try
                {
                    //TODO Have variables to fill out how many customers, accounts and total sum of all accounts money.
                    Console.WriteLine($"\n\n\n\t HUVUDMENY" +
                                                   $"\n\t 0) Avsluta och spara" +
                                                   $"\n\t 1) Sök kund" +
                                                   $"\n\t 2) Visa kundbild" +
                                                   $"\n\t 3) Skapa kund" +
                                                   $"\n\t 4) Ta bort kund" +
                                                   $"\n\t 5) Skapa konto" +
                                                   $"\n\t 6) Ta bort konto" +
                                                   $"\n\t 7) Insättning" +
                                                   $"\n\t 8) Uttag" +
                                                   $"\n\t 9) Överföring");
                    var menySelction = int.Parse(Console.ReadLine());
                    var customerId = 0;
                    var accountId = 0;
                    var sum = 0;
                    switch (menySelction)
                    {
                        case 0:

                            isRunning = false;
                            break;
                        case 1:
                            Console.Write("\n\tSök efter kund genom att skriva in namn på kund eller postort som kunden bor på: ");
                            var searchString = Console.ReadLine();
                            //TODO Add a service to search after customer.
                            break;

                        case 2:
                            Console.Write("\n\tSkriv in kundnummer för att få kundbild: ");
                            customerId = int.Parse(Console.ReadLine());
                            //TODO Add a service to get customer info.
                            break;

                        case 3:
                            Console.Write("\n\tFör in uppgifter för att skapa kund: \n\tNamn: ");
                            var name = Console.ReadLine();
                            Console.Write("\tPersonnummer: ");
                            var legalId = Console.ReadLine();
                            Console.Write("\tGatuadress: ");
                            var address = Console.ReadLine();
                            Console.Write("\tPostnummer: ");
                            var postalCode = Console.ReadLine();
                            Console.Write("\tOmråde: ");
                            var area = Console.ReadLine();
                            //TODO Add a service to add a customer.
                            break;

                        case 4:
                            Console.Write("\n\tSkriv in kundnummer för den du vill radera: ");
                            customerId = int.Parse(Console.ReadLine());
                            //TODO Add a service to delete a customer.
                            break;

                        case 5:
                            Console.Write("\n\tAnge kundnummer där du vill att nya kontot skapas: ");
                            customerId = int.Parse(Console.ReadLine());
                            //TODO Add a service to create a account.
                            break;

                        case 6:
                            Console.Write("\n\tAnge kontonummer för det konto du vill radera: ");
                            accountId = int.Parse(Console.ReadLine());
                            //TODO Add a service to delete an account.
                            break;

                        case 7:
                            Console.Write("\n\tUppge summa för insätting:");
                            sum = int.Parse(Console.ReadLine());
                            Console.Write("\tUppge kontonummer som insättningen kommer gå till: ");
                            accountId = int.Parse(Console.ReadLine());
                            //TODO Add a service to deposit money to an account.
                            break;

                        case 8:
                            Console.Write("\n\tAnge summa som ska tas ut: ");
                            sum = int.Parse(Console.ReadLine());
                            Console.Write("\tUppge vilket kontonummer ska dras från: ");
                            accountId = int.Parse(Console.ReadLine());
                            //TODO Add a service for withdraw money from a account.
                            break;

                        case 9:
                            Console.Write("\n\tAnge summa för transaktion: ");
                            var sumForTranscation = int.Parse(Console.ReadLine());
                            Console.Write("\tUppge kontonummer som ska debiteras: ");
                            var withdrawAccountId = int.Parse(Console.ReadLine());
                            Console.Write("\tUppge kontonummer som ska ta emot summa: ");
                            var depositAccountId = int.Parse(Console.ReadLine());
                            //TODO Add a service for transaction between accounts.
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Kommando finns inte.");
                }
            }
        }
    }
}
