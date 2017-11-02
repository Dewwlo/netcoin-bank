using System;
using System.Collections.Generic;
using System.Text;
using NetcoinLib.Models;

namespace NetcoinLib
{
    public interface INetcoinRepository
    {
        List<Customer> GetCustomers();
        List<Account> GetAccounts();
        void Save();
    }
}
