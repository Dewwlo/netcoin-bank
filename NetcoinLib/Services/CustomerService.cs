using NetcoinLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetcoinLib.Services
{
    public class CustomerService
    {
        private INetcoinRepository repository;
        public CustomerService(INetcoinRepository repository)
        {
            this.repository = repository;
        }
        public bool CreateCustomer(string Name, string LegalId, string Area = null, string Address = null, string PostalCode = null)
        {
            try
            {
                int highId = 0;
                if (repository.GetCustomers().Count > 0)
                {
                    highId = repository.GetCustomers().OrderByDescending(x => x.CustomerId).First().CustomerId;
                }
                Customer customer = new Customer
                {
                    CustomerId = highId + 1,
                    LegalId = LegalId,
                    Name = LegalId,
                    Area = Area,
                    Address = Address,
                    PostalCode = PostalCode,
                    Accounts = new List<Account>()
                };
                // TODO Replace transaction account with real CreateAccount once method is made
                Account transaction = new Account { Balance = 0, Customer = customer };
                customer.Accounts.Add(transaction);
                repository.GetCustomers().Add(customer);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            
        }
        public void GetCustomer(string search)
        {

        }
    }
}
