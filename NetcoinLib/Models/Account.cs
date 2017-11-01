using System;
using System.Collections.Generic;
using System.Text;

namespace NetcoinLib.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public decimal Balance { get; set; }
        public bool CanDelete => throw new NotImplementedException();
    }
}
