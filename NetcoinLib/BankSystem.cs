using NetcoinLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetcoinLib
{
    public class BankSystem
    {
        public List<Customer> Customers { get; set; }
        public List<Account> Accounts { get; set; }

        private readonly INetcoinRepository _netcoinRepository;
        public BankSystem(INetcoinRepository netcoinRepository)
        {
            _netcoinRepository = netcoinRepository;
        }

        public void ReadTextFile(string fileName) => _netcoinRepository.ReadSerializedData(fileName);

        public void SaveTextFile() => _netcoinRepository.Save();

        public void WithdrawFromAccount(int accountId, decimal amountToWithdraw)
        {
            Account account = _netcoinRepository.GetAccounts().SingleOrDefault(x => x.AccountId == accountId);
            if (account != null)
            {
                if (account.Balance >= amountToWithdraw)
                    account.Balance = account.Balance - amountToWithdraw;
                else
                    throw new Exception($"Not enough balance ({ account.Balance }) in account with ID { accountId } in order to withdraw { amountToWithdraw }");
            }
            else
            {
                throw new NullReferenceException();
            }
        }
    }
}
