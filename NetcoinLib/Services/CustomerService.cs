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
            return _context.GetCustomers().Where((c => search == c.Area || search == c.Name)).ToList();
        }
    }
}
