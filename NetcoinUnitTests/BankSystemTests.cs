using NetcoinLib;
using NetcoinUnitTests.Models;
using NetcoinUnitTests.Repositories;
using NetcoinUnitTests.Utilities;
using System;
using System.Linq;
using Xunit;

namespace NetcoinUnitTests
{
    public class BankSystemTests
    {
        private readonly INetcoinRepository _repository;
        public BankSystemTests()
        {
            _repository = new FakeNetcoinRepository();
        }

        [Fact]
        public void WithdrawFromAccount()
        {
            //Assemble
            NetcoinRepoRepresentation representation = NetcoinRepositoryUtility.CreateSampleCustomersAndAccounts(5, 100M);
            _repository.GetAccounts().AddRange(representation.Accounts);
            BankSystem sut = new BankSystem(_repository);
            sut.Initialize();

            //Act
            sut.WithdrawFromAccount(2, 100M);

            //Act & assert
            Assert.True(_repository.GetAccounts().Single(x => x.AccountId == 2).Balance == 0);
            Assert.Throws<InvalidOperationException>(() => sut.WithdrawFromAccount(1, 101M));
            Assert.Throws<ArgumentOutOfRangeException>(() => sut.WithdrawFromAccount(1, -1M));
            Assert.Throws<NullReferenceException>(() => sut.WithdrawFromAccount(100, 1M));
        }

        [Fact]
        public void CetCustomerByCustomerId()
        {
            NetcoinRepoRepresentation representation = NetcoinRepositoryUtility.CreateSampleCustomersAndAccounts(5);
            INetcoinRepository fakeProvider = new FakeNetcoinRepository();
            fakeProvider.GetCustomers().AddRange(representation.Customers);
            BankSystem sut = new BankSystem(fakeProvider);
            sut.Initialize();

            var result1 = sut.GetCustomerById("1");

            Assert.Throws<NullReferenceException>(() => sut.GetCustomerById("0"));
            Assert.Equal(1, result1.CustomerId);
        }
    }
}
