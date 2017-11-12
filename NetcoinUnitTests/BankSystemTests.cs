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
            sut.WithdrawFromAccount(1, 101M);
            sut.WithdrawFromAccount(1, -1M);
            sut.WithdrawFromAccount(100, 1M);
            //Assert.Throws<InvalidOperationException>(() => sut.WithdrawFromAccount(1, 101M));
            //Assert.Throws<ArgumentOutOfRangeException>(() => sut.WithdrawFromAccount(1, -1M));
            //Assert.Throws<NullReferenceException>(() => sut.WithdrawFromAccount(100, 1M));
        }

        [Fact]
        public void DepositToAccount()
        {
            //Assemble
            NetcoinRepoRepresentation representation = NetcoinRepositoryUtility.CreateSampleCustomersAndAccounts(5, 0M);
            //INetcoinRepository fakeProvider = new FakeNetcoinRepository();
            _repository.GetAccounts().AddRange(representation.Accounts);
            BankSystem sut = new BankSystem(_repository);
            sut.Initialize();

            //Act
            sut.DepositToAccount(2, 100M);

            //Act & assert
            Assert.True(_repository.GetAccounts().Single(x => x.AccountId == 2).Balance == 100);
            sut.WithdrawFromAccount(1, -1M);
            sut.WithdrawFromAccount(100, 1M);
            //Assert.Throws<ArgumentOutOfRangeException>(() => sut.WithdrawFromAccount(1, -1M));
            //Assert.Throws<NullReferenceException>(() => sut.WithdrawFromAccount(100, 1M));
        }
    }
}
