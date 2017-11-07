using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using NetcoinLib;
using NetcoinLib.Models;

namespace NetcoinDbLib
{
    public class SerializationRepository : INetcoinRepository
    {
        private List<Customer> Customers { get; set; }
        private List<Account> Accounts { get; set; }

        public void ReadSerializedData(string fileName)
        {
            var path = "C:/Users/a_bjo/Desktop/skolprojekt/ALM/NetcoinBank/netcoin-bank/data/" + fileName;
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                }
            }
        }

        public List<Customer> GetCustomers() => Customers;

        public List<Account> GetAccounts() => Accounts;
        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
