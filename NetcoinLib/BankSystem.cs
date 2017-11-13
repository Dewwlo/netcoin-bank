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

        public bool CreateCustomer(string name, string legalId, string area, string address, string postalCode, string city, string country, string phoneNumber) => _customerService.CreateCustomer(name, legalId, area, address, postalCode, city, country, phoneNumber);

        public bool RemoveCustomer(int customerId)
        {
            Customer customer = Customers.SingleOrDefault(x => x.CustomerId == customerId);
            if (customer != null)
                return _customerService.RemoveCustomer(customer);
            else
                return false;
        }

        public Customer GetCustomerById(string search)
        {
            var result = _customerService.GetCustomerByCustomerId(search);
            if (result != null || result.CustomerId != 0)
            {
                return result;
            }
            else
            {
                throw new NullReferenceException("Kund id:et du söker finns inte.");
            }
        }

        public List<Customer> GetCustomerByNameOrArea(string search) => _customerService.SearchAfterCustomerWithAreaOrName(search);

        public string WithdrawFromAccount(int accountId, decimal amountToWithdraw)
        {
            try
            {
                _accountService.Withdraw(accountId, amountToWithdraw);
                return $"Withdrew {amountToWithdraw} from {accountId}";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string DepositToAccount(int accountId, decimal amountToDeposit)
        {
            try
            {
                _accountService.Deposit(accountId, amountToDeposit);
                return $"Deposited {amountToDeposit} to {accountId}";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public bool TransferMoneyBetweenAccounts(int fromAccountId, int toAccountId, decimal amount)
        {
            try
            {
                _accountService.TransferMoneyBetweenAccounts(fromAccountId, toAccountId, amount);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CreateAccount(int customerId) => _accountService.CreateAccount(customerId);

        public bool RemoveAccount(int accountNumber)
        {
            var account = Accounts.FirstOrDefault(a=> accountNumber == a.AccountId);            
            return _accountService.RemoveAccount(account);
        }
    }
}