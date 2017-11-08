using NetcoinLib.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetcoinUnitTests.Models
{
    public class NetcoinRepoRepresentation
    {
        public List<Customer> Customers { get; set; } = new List<Customer>();
        public List<Account> Accounts { get; set; } = new List<Account>();
    }
}
