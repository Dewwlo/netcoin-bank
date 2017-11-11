using System;
using System.Collections.Generic;
using System.Linq;
using NetcoinLib.Models;
using NetcoinLib.Services;

namespace NetcoinLib
{
    public class BankSystem
    {
        public List<Customer> Customers { get; set; }
        public List<Account> Accounts { get; set; }
        public decimal TotalBalance { get; set; }

        private readonly INetcoinRepository _netcoinRepository;
        private CustomerService _customerService;
        public BankSystem(INetcoinRepository netcoinRepository)
        {
            _netcoinRepository = netcoinRepository;
            _customerService = new CustomerService(_netcoinRepository);
        }

        public void ReadTextFile(string fileName) => _netcoinRepository.ReadSerializedData(fileName);

        public void SaveTextFile() => _netcoinRepository.Save();

        public void Initialize()
        {
            Customers = _netcoinRepository.GetCustomers();
            Accounts = _netcoinRepository.GetAccounts();
            TotalBalance = Accounts.Sum(a => a.Balance);
        }

        public List<Customer> GetCustomerByNameOrArea(string search) => _customerService.SearchAfterCustomerWithAreaOrName(search);

        public void WithdrawFromAccount(int accountId, decimal amountToWithdraw)
        {
            Account account = Accounts.SingleOrDefault(x => x.AccountId == accountId);
            if (account != null)
            {
                if (account.Balance < amountToWithdraw)
                    throw new InvalidOperationException("The account balance is less than the amount attempted to withdraw.");
                else if (amountToWithdraw < 0.1M)
                    throw new ArgumentOutOfRangeException("The amount to withdraw must be greater than 0.1.");
                else
                    account.Balance = account.Balance - amountToWithdraw;
            }
            else
            {
                throw new NullReferenceException("Account not found.");
            }
        }
    }
}
