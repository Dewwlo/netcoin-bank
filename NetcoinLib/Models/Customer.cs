﻿using System.Collections.Generic;
using System.Linq;

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
        public string City { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Account> Accounts { get; set; }
        public bool CanDelete {
            get { return Accounts.Sum(x => x.Balance) == 0M; }
        }
    }
}
