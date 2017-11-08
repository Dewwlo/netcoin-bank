using System;
using System.Collections.Generic;
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
        private List<Customer> Customers { get; set; }
        private List<Account> Accounts { get; set; }

        private SerializationRepository()
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
