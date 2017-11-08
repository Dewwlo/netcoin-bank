using NetcoinLib;
using System;
using System.Collections.Generic;
using System.Text;
using NetcoinLib.Models;

namespace NetcoinUnitTests.Repositories
{
    public class FakeNetcoinRepository : INetcoinRepository
    {
        private List<Account> _accounts = new List<Account>();
        private List<Customer> _customers = new List<Customer>();

        public List<Account> GetAccounts()
        {
            return _accounts;
        }

        public List<Customer> GetCustomers()
        {
            return _customers;
        }

        public void Save()
        { }
    }
}
