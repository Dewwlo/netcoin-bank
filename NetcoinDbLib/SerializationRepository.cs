using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using NetcoinLib;
using NetcoinLib.Models;

namespace NetcoinDbLib
{
    public class SerializationRepository : INetcoinRepository
    {
        private static INetcoinRepository instance { get; set; }
        public static INetcoinRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SerializationRepository();
                }
                return instance;
            }
        }

        private static readonly Assembly Assembly = Assembly.GetExecutingAssembly();
        private readonly string _path = System.IO.Path.GetDirectoryName(Assembly.Location)+ "\\data\\";
        private string _fileName; 
        private List<Customer> Customers { get; } = new List<Customer>();
        private List<Account> Accounts { get; } = new List<Account>();

        private SerializationRepository()
        {
            
        }

        public List<Customer> GetCustomers() => Customers;

        public List<Account> GetAccounts() => Accounts;

        public void ReadSerializedData(string fileName)
        {
            _fileName =  fileName;
            using (StreamReader reader = new StreamReader(_path + fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var count = line.Count(c => c == ';');
                    
                    if (count == 8)
                        ReadCustomer(line);
                    else if (count == 2)
                        ReadAccount(line);
                }
            }
        }
        public void Save()
        {
            using (var writer = new StreamWriter(_path + _fileName))
            {
                WriteToFile(writer);
            }

            using (var writer = new StreamWriter($"{_path}{DateTime.UtcNow:yyyyMMdd-HHmm}.txt"))
            {
                WriteToFile(writer);
            }
        }

        private void WriteToFile(StreamWriter writer)
        {
            writer.WriteLine(Customers.Count);
            Customers.ForEach(c =>
            {
                writer.WriteLine($"{c.CustomerId};{c.LegalId};{c.Name};" +
                                 $"{c.Address};{c.City};{c.Area};{c.PostalCode};" +
                                 $"{c.Country};{c.PhoneNumber}");
            });
            writer.WriteLine(Accounts.Count);
            Accounts.ForEach(a =>
            {
                writer.WriteLine($"{a.AccountId};{a.CustomerId};{a.Balance}");
            });
        }

        private void ReadCustomer(string customer)
        {
            string[] substrings = Regex.Split(customer, ";");
            Customers.Add(new Customer
            {
                CustomerId = Int32.Parse(substrings[0]),
                LegalId = substrings[1],
                Name = substrings[2],
                Address = substrings[3],
                City = substrings[4],
                Area = substrings[5],
                PostalCode = substrings[6],
                Country = substrings[7],
                PhoneNumber = substrings[8]
            });
        }
        private void ReadAccount(string account)
        {
            string[] substrings = Regex.Split(account, ";");
            Accounts.Add(new Account
            {
                AccountId = Int32.Parse(substrings[0]),
                CustomerId = Int32.Parse(substrings[1]),
                Balance = decimal.Parse(substrings[2]),
                Customer = Customers.SingleOrDefault(c => c.CustomerId == Int32.Parse(substrings[1]))
            });
        }
    }
}
