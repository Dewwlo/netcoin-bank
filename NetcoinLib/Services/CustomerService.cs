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
                    Name = Name,
                    Area = Area,
                    Address = Address,
                    PostalCode = PostalCode,
                    City = City,
                    Country = Country,
                    PhoneNumber = PhoneNumber,
                    Accounts = new List<Account>()
                };
                AccountService aService = new AccountService(repository);
                
                var result = aService.CreateAccount(customer);

                if (result)
                    repository.GetCustomers().Add(customer);
                
                return result;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public void GetCustomer(string search) { }

        public List<Customer> SearchAfterCustomerWithAreaOrName(string search)
        {
            if (search == "")
            {
                return new List<Customer>();
            }
            else
            {
                return repository.GetCustomers().Where((c => c.Area.Contains(search) || c.Name.Contains(search))).ToList();
            }            
        }

        public bool RemoveCustomer(Customer customer)
        {
            if (customer.CanDelete)
            {
                foreach (Account account in customer.Accounts)
                {
                    repository.GetAccounts().Remove(account);
                }

                repository.GetCustomers().Remove(customer);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
