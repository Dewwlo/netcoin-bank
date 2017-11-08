using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using NetcoinLib;
using NetcoinLib.Models;

namespace NetcoinDbLib
{
    public class SerializationRepository : INetcoinRepository
    {
        private List<Customer> Customers { get; } = new List<Customer>();
        private List<Account> Accounts { get; } = new List<Account>();

        public void ReadSerializedData(string fileName)
        {
            var path = "C:/Users/a_bjo/Desktop/skolprojekt/ALM/NetcoinBank/data/" + fileName;
            using (StreamReader reader = new StreamReader(path))
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

        public List<Customer> GetCustomers() => Customers;

        public List<Account> GetAccounts() => Accounts;
        public void Save()
        {
            throw new NotImplementedException();
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
