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
        private readonly CustomerService _customerService;
        private readonly AccountService _accountService;
        public BankSystem(INetcoinRepository netcoinRepository)
        {
            _netcoinRepository = netcoinRepository;
            _customerService = new CustomerService(_netcoinRepository);
            _accountService = new AccountService(_netcoinRepository);
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

        public bool TransferMoneyBetweenAccounts(int toAccountId, int fromAccountId, decimal amount) =>
            _accountService.TransferMoneyBetweenAccounts(toAccountId, fromAccountId, amount);
    }
}
