using NetcoinLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetcoinLib.Services
{
    public class CustomerService
    {
        private readonly INetcoinRepository _context;

        public CustomerService(INetcoinRepository context)
        {
            _context = context;
        }

        public List<Customer> SearchAfterCustomerWithAreaOrName(string search)
        {
            if (search == "")
            {
                return new List<Customer>();
            }
            else
            {
                return _context.GetCustomers().Where((c => c.Area.Contains(search) || c.Name.Contains(search))).ToList();
            }            
        }
    }
}
