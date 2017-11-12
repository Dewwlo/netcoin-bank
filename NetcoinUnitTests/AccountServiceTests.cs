using System.Linq;
using NetcoinLib;
using NetcoinLib.Models;
using NetcoinLib.Services;
using NetcoinUnitTests.Repositories;
using NetcoinUnitTests.Utilities;
using Xunit;

namespace NetcoinUnitTests
{

    public class AccountServiceTests
    {
        private readonly INetcoinRepository _repository;
        public AccountServiceTests()
        {
            _repository = new FakeNetcoinRepository();
        }

        [Fact]
        public void CanCreateAccount()
        {

            CustomerService cService = new CustomerService(_repository);
            cService.CreateCustomer("John Doe", "101112-1234", "Stockholms Län", "Gustav Adolfs Torg 1", "12312", "Stockholm", "Sverige", "070-123 123 123");
            Customer customer = _repository.GetCustomers().Find(x => x.LegalId == "101112-1234");
            AccountService sut = new AccountService(_repository);
            
            bool result = sut.CreateAccount(customer);
            Assert.True(result);

            result = sut.CreateAccount(customer.CustomerId);
            Assert.True(result);
        }

        [Fact]
        public void DeleteAccountValidatesAndRemoves()
        {
            //Assemble
            var setupData = NetcoinRepositoryUtility.CreateSampleCustomersAndAccounts(1, 100M);
            _repository.GetAccounts().AddRange(setupData.Accounts);
            Account account1 = setupData.Accounts[0];
            Account account2 = setupData.Accounts[1];
            AccountService sut = new AccountService(_repository);

            //Act & assert
            Assert.False(sut.RemoveAccount(account1));
            account1.Balance = 0M;
            Assert.True(sut.RemoveAccount(account1));
            Assert.False(sut.RemoveAccount(account2));
            Assert.True(setupData.Customers[0].Accounts.Count == 1 && _repository.GetAccounts().Count == 1);
        }

        [Fact]
        public void CanTransferMoney()
        {
            CustomerService cService = new CustomerService(_repository);
            cService.CreateCustomer("John Doe", "101112-1234", "Stockholms Län", "Gustav Adolfs Torg 1", "12312", "Stockholm", "Sverige", "070-123 123 123");
            cService.CreateCustomer("Jane Doe", "101010-4321", "Stockholms Län", "Gustav Adolfs Torg 1", "12312", "Stockholm", "Sverige", "070-123 123 124");
            var fromAccount = _repository.GetAccounts().FirstOrDefault();
            var toAccount = _repository.GetAccounts().Skip(1).FirstOrDefault();
            fromAccount.Balance = 1000M;
            AccountService sut = new AccountService(_repository);

            var result = sut.TransferMoneyBetweenAccounts(fromAccount.AccountId, toAccount.AccountId, 500M);
            Assert.True(result);

            result = sut.TransferMoneyBetweenAccounts(fromAccount.AccountId, toAccount.AccountId, 501M);
            Assert.False(result);

            result = sut.TransferMoneyBetweenAccounts(fromAccount.AccountId, toAccount.AccountId, -501M);
            Assert.False(result);

            Assert.Equal(fromAccount.Balance, 500);
        }
    }
}
