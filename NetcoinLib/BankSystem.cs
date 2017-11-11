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
            Account account = _netcoinRepository.GetAccounts().SingleOrDefault(x => x.AccountId == accountId);
            if (account != null)
            {
                if (account.Balance >= amountToWithdraw && amountToWithdraw > 0.1M)
                    account.Balance = account.Balance - amountToWithdraw;
                else
                    throw new Exception($"Negative amount, or not enough balance ({ account.Balance }) in account with ID { accountId } in order to withdraw { amountToWithdraw }");
            }
            else
            {
                throw new NullReferenceException();
            }
        }
    }
}
