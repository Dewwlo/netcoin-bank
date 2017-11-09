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
        public bool CreateCustomer(string Name, string LegalId, string Area, string Address, string PostalCode, string City, string Country, string PhoneNumber)
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
                    City = City,
                    Country = Country,
                    PhoneNumber = PhoneNumber,
                    Accounts = new List<Account>()
                };
                AccountService aService = new AccountService(repository);
                bool success = aService.CreateAccount(customer);
                if (success)
                {
                    Account transactionAccount = repository.GetAccounts().Find(x => x.CustomerId == customer.CustomerId);
                    repository.GetAccounts().Add(transactionAccount);
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
