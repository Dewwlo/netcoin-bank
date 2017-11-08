using NetcoinLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetcoinLib
{
    public class BankSystem
    {
        private readonly INetcoinRepository _netcoinRepository;
        public BankSystem(INetcoinRepository netcoinRepository)
        {
            _netcoinRepository = netcoinRepository;
        }

        public void WithdrawFromAccount(int accountId, decimal amountToWithdraw)
        {
            Account account = _netcoinRepository.GetAccounts().SingleOrDefault(x => x.AccountId == accountId);
            if (account != null)
            {
                if (account.Balance >= amountToWithdraw)
                    account.Balance = account.Balance - amountToWithdraw;
            }
            else
            {
                throw new NullReferenceException();
            }
        }
    }
}
