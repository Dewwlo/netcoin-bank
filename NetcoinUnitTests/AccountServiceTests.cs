using System;
using System.Linq;
using NetcoinLib;
using NetcoinLib.Models;
using NetcoinLib.Services;
using NetcoinUnitTests.Repositories;
using NetcoinUnitTests.Utilities;
using Xunit;
using NetcoinUnitTests.Models;

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
            //Assemble
            CustomerService cService = new CustomerService(_repository);
            cService.CreateCustomer("John Doe", "101112-1234", "Stockholms Län", "Gustav Adolfs Torg 1", "12312", "Stockholm", "Sverige", "070-123 123 123");
            cService.CreateCustomer("Jane Doe", "101010-4321", "Stockholms Län", "Gustav Adolfs Torg 1", "12312", "Stockholm", "Sverige", "070-123 123 124");
            var fromAccount = _repository.GetAccounts().FirstOrDefault();
            var toAccount = _repository.GetAccounts().Skip(1).FirstOrDefault();
            AccountService sut = new AccountService(_repository);

            //Act
            fromAccount.Balance = 1000M;
            sut.TransferMoneyBetweenAccounts(fromAccount.AccountId, toAccount.AccountId, 500M);

            //Act & Assert
            Assert.Equal(fromAccount.Balance, 500);
            Assert.Throws<InvalidOperationException>(() => sut.TransferMoneyBetweenAccounts(fromAccount.AccountId, toAccount.AccountId, 501M));
            Assert.Throws<InvalidOperationException>(() => sut.TransferMoneyBetweenAccounts(fromAccount.AccountId, toAccount.AccountId, -501M));
            Assert.Throws<NullReferenceException>(() => sut.TransferMoneyBetweenAccounts(1,6, 200M));
        }
        [Fact]
        public void WithdrawFromAccount()
        {
            //Assemble
            NetcoinRepoRepresentation representation = NetcoinRepositoryUtility.CreateSampleCustomersAndAccounts(5, 100M);
            INetcoinRepository fakeProvider = new FakeNetcoinRepository();
            fakeProvider.GetAccounts().AddRange(representation.Accounts);
            AccountService sut = new AccountService(fakeProvider);
            BankSystem bank = new BankSystem(fakeProvider);
            bank.Initialize();

            //Act
            sut.Withdraw(2, 100M);

            //Act & assert
            Assert.True(fakeProvider.GetAccounts().Single(x => x.AccountId == 2).Balance == 0);
            Assert.Throws<InvalidOperationException>(() => sut.Withdraw(1, 101M));
            Assert.Throws<ArgumentOutOfRangeException>(() => sut.Withdraw(1, -1M));
            Assert.Throws<NullReferenceException>(() => sut.Withdraw(100, 1M));
        }

        [Fact]
        public void DepositToAccount()
        {
            //Assemble
            NetcoinRepoRepresentation representation = NetcoinRepositoryUtility.CreateSampleCustomersAndAccounts(5, 0M);
            INetcoinRepository fakeProvider = new FakeNetcoinRepository();
            fakeProvider.GetAccounts().AddRange(representation.Accounts);
            AccountService sut = new AccountService(fakeProvider);
            BankSystem bank = new BankSystem(fakeProvider);
            bank.Initialize();

            //Act
            sut.Deposit(2, 100M);

            //Act & assert
            Assert.True(fakeProvider.GetAccounts().Single(x => x.AccountId == 2).Balance == 100);
            Assert.Throws<ArgumentOutOfRangeException>(() => sut.Withdraw(1, -1M));
            Assert.Throws<NullReferenceException>(() => sut.Withdraw(100, 1M));
        }
    }
}
