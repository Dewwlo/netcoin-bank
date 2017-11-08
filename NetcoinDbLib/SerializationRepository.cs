using System;
using System.Collections.Generic;
using NetcoinLib;
using NetcoinLib.Models;

namespace NetcoinDbLib
{
    public class SerializationRepository : INetcoinRepository
    {
        private List<Customer> Customers { get; set; }
        private List<Account> Accounts { get; set; }

        SerializationRepository()
        {
            ReadSerializedData();
        }

        public void ReadSerializedData()
        {
            //gets serialized data and updates lists
        }

        public void WriteSerializedData()
        {
            //writes serialized data when closing application
        }

        public List<Customer> GetCustomers() => Customers;

        public List<Account> GetAccounts() => Accounts;

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
