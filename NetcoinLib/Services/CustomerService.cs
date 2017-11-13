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
        private List<Customer> Customers
        {
            get
            {
                return repository.GetCustomers();
            }
        }
        private List<Account> Accounts
        {
            get
            {
                return repository.GetAccounts();
            }
        }
        public bool CreateCustomer(string Name, string LegalId, string Area, string Address, string PostalCode, string City, string Country, string PhoneNumber)
        {
            if (String.IsNullOrWhiteSpace(Name) || String.IsNullOrWhiteSpace(LegalId) || String.IsNullOrWhiteSpace(Area) || String.IsNullOrWhiteSpace(PostalCode)
                || String.IsNullOrWhiteSpace(City) || String.IsNullOrWhiteSpace(Country) || String.IsNullOrWhiteSpace(PhoneNumber))
                return false;

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
                    Customers.Add(customer);
                
                return result;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public Customer GetCustomerByCustomerId(string search)
        {
            if (search == "")
            {
                return new Customer();
            }
            else
            {
                return Customers.FirstOrDefault(c=> search == c.CustomerId.ToString());
            }
        }

        public List<Customer> SearchAfterCustomerWithAreaOrName(string search)
        {
            search = search.ToLower();
            if (search == "")
            {
                return new List<Customer>();
            }
            else
            {
                return Customers.Where((c => c.Area.ToLower().Contains(search) || c.Name.ToLower().Contains(search))).ToList();
            }            
        }

        public bool RemoveCustomer(Customer customer)
        {
            if (customer.CanDelete)
            {
                foreach (Account account in customer.Accounts)
                {
                    Accounts.Remove(account);
                }

                Customers.Remove(customer);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
