using NetcoinLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                // TODO Replace transaction account with real CreateAccount once method is made
                Account transactionAccount = new Account { Balance = 0, Customer = customer };
                customer.Accounts.Add(transactionAccount);
                repository.GetCustomers().Add(customer);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
        public void GetCustomer(string search)
        {

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
