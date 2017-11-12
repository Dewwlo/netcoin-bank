using NetcoinLib.Models;
using System;
using System.Linq;

namespace NetcoinLib.Services
{
    public class AccountService
    {
        private INetcoinRepository repository;
        public AccountService(INetcoinRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Creates an account tied to a specific Customer
        /// </summary>
        /// <param name="customer">The customer whose account this shall be</param>
        /// <returns>True if creation is successful, otherwise false</returns>
        public bool CreateAccount(Customer customer)
        {
            try
            {
                int highId = 0;
                if (repository.GetAccounts().Count > 0)
                {
                    highId = repository.GetAccounts().OrderByDescending(x => x.CustomerId).First().CustomerId;
                }

                Account account = new Account {
                    Balance = 0,
                    CustomerId = customer.CustomerId,
                    Customer = customer,
                    AccountId = highId + 1
                };

                repository.GetAccounts().Add(account);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Finds the customer with a specific id, and calls CreateAccount with that customer
        /// </summary>
        /// <param name="customerId">An Id for a Customer whose account this shall be</param>
        /// <returns>True if creation is successful, otherwise false</returns>
        public bool CreateAccount(int customerId)
        {
            Customer customer = repository.GetCustomers().Find(x => x.CustomerId == customerId);
            return CreateAccount(customer);
        }

        public bool RemoveAccount(Account account)
        {
            if (account.CanDelete)
            {
                repository.GetAccounts().Remove(account);
                account.Customer.Accounts.Remove(account);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
