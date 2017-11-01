using System;
using System.Collections.Generic;
using System.Text;

namespace NetcoinLib.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string LegalId { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Area { get; set; }
        public ICollection<Account> Accounts { get; set; }
        public bool CanDelete => throw new NotImplementedException();
    }
}
