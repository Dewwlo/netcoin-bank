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
                AccountService aService = new AccountService(repository);
                bool success = aService.CreateAccount(customer);
                if (success)
                {
                    Account transaction = repository.GetAccounts().Find(x => x.CustomerId == customer.CustomerId);
                    repository.GetAccounts().Add(transaction);
                }
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
