using NetcoinLib.Models;
using System;
using System.Collections.Generic;
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
        private List<Account> Accounts
        {
            get
            {
                return repository.GetAccounts();
            }
        }
        private List<Customer> Customers
        {
            get
            {
                return repository.GetCustomers();
            }
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
                    highId = repository.GetAccounts().OrderByDescending(x => x.AccountId).First().AccountId;
                }

                Account account = new Account {
                    Balance = 0,
                    CustomerId = customer.CustomerId,
                    Customer = customer,
                    AccountId = highId + 1
                };

                Accounts.Add(account);
                customer.Accounts.Add(account);
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
            Customer customer = Customers.Find(x => x.CustomerId == customerId);
            return CreateAccount(customer);
        }

        public bool RemoveAccount(Account account)
        {
            if (account.CanDelete)
            {
                Accounts.Remove(account);
                account.Customer.Accounts.Remove(account);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Withdraws an amount from a provided account
        /// </summary>
        /// <param name="accountId">The Id of the withdraw account</param>
        /// <param name="amountToWithdraw">The sum to be withdrawn</param>
        public void Withdraw(int accountId, decimal amountToWithdraw)
        {
            Account account = Accounts.SingleOrDefault(x => x.AccountId == accountId);
            if (account == null)
                throw new NullReferenceException("Account not found.");
            if (account.Balance < amountToWithdraw)
                throw new InvalidOperationException("The account balance is less than the amount attempted to withdraw.");
            if (amountToWithdraw < 0.1M)
                throw new ArgumentOutOfRangeException("The amount to withdraw must be greater than 0.1.");

            account.Balance = account.Balance - amountToWithdraw;
        }
        /// <summary>
        /// Deposits an amount to a provided account
        /// </summary>
        /// <param name="accountId">The account to deposit to</param>
        /// <param name="amountToDeposit">The amount to deposit</param>
        public void Deposit(int accountId, decimal amountToDeposit)
        {
            Account account = Accounts.SingleOrDefault(x => x.AccountId == accountId);
            if (account == null)
                throw new NullReferenceException("Account not found.");
            if (amountToDeposit < 0.1M)
                throw new ArgumentOutOfRangeException("The amount to deposit must be greater than 0.1.");

            account.Balance = account.Balance + amountToDeposit;
        }

        public bool TransferMoneyBetweenAccounts(int fromAccountId, int toAccountId, decimal amount)
        {
            var fromAccount = Accounts.SingleOrDefault(a => a.AccountId == fromAccountId);
            var toAccount = Accounts.SingleOrDefault(a => a.AccountId == toAccountId);

            if (ValidateTransfer(amount, fromAccount, toAccount))
            {
                fromAccount.Balance -= amount;
                toAccount.Balance += amount;
                return true;
            }
                    
            return false;
        }

        private bool ValidateTransfer(decimal amount, Account fromAccount, Account toAccount)
        {
            return fromAccount != null && toAccount != null && (amount >= 0.1M && fromAccount.Balance >= amount);
        }
    }
}
